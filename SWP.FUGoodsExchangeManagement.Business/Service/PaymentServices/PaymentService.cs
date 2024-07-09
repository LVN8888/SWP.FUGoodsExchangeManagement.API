using Microsoft.AspNetCore.Http;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Business.VnPayService;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Request;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.VnPayDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVnPayService _vnPayService;

        public PaymentService(IUnitOfWork unitOfWork, IVnPayService vnPayService)
        {
            _unitOfWork = unitOfWork;
            _vnPayService = vnPayService;
        }

        public async Task<Payment> UpdatePaymentStatus(PaymentUpdateRequestModel model)
        {
            var currentPayment = await _unitOfWork.PaymentRepository.GetById(model.Id);

            if (currentPayment == null)
            {
                throw new CustomException("Payment Id does not exist");
            }

            if (!Enum.IsDefined(typeof(PaymentStatus), model.Status)) 
            {
                throw new CustomException("Payment status is invalid");
            }

            currentPayment.Status = model.Status;

            _unitOfWork.PaymentRepository.Update(currentPayment);

            // only save change if status is not Success
            // if status is Success, save change after extend post successfully
            if (!model.Status.Equals(nameof(PaymentStatus.Success)))
            {
                await _unitOfWork.SaveChangeAsync();
            }
            
            return currentPayment;
        }

        public async Task<string> GetPaymentUrl(HttpContext context, string paymentId, string redirectUrl)
        {
            var currentPayment = await _unitOfWork.PaymentRepository.GetById(paymentId);

            if (currentPayment == null)
            {
                throw new CustomException("Payment does not exist!");
            }

            if (currentPayment.Status.Equals(nameof(PaymentStatus.Success)))
            {
                throw new CustomException("Payment has already been paid!");
            }

            if (!currentPayment.ProductPost.Status.Equals(nameof(ProductPostStatus.Unpaid)))
            {
                throw new CustomException("The post of this payment does not in unpaid status");
            }

            VnPaymentRequestModel vnpay = new VnPaymentRequestModel
            {
                OrderId = currentPayment.ProductPost.Id,
                PaymentId = currentPayment.Id,
                Amount = decimal.Parse(currentPayment.Price),
                CreatedDate = currentPayment.PaymentDate,
                RedirectUrl = redirectUrl
            };

            return _vnPayService.CreatePaymentUrl(context, vnpay);
        }
    }
}
