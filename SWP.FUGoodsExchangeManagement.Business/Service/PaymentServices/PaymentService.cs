using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Request;
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

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Payment> UpdatePaymentStatus(PaymentUpdateRequestModel model)
        {
            var currentPayment = await _unitOfWork.PaymentRepository.GetByID(model.Id);

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
    }
}
