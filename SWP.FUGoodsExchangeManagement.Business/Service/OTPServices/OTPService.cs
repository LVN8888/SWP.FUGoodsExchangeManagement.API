using SWP.FUGoodsExchangeManagement.Business.Service.MailServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.OTPDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.OTPServices
{
    public class OTPService : IOTPService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _emailService;

        public OTPService(IUnitOfWork unitOfWork, IMailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        private string CreateNewOTPCode()
        {
            return new Random().Next(0, 999999).ToString("D6");
        }

        public async Task CreateOTPCodeForEmail(OTPSendEmailRequestModel model)
        {
            User currentUser = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(model.Email));

            if (currentUser == null)
            {
                throw new CustomException("There is no account with this email");
            }

            var latestOTP = await _unitOfWork.OTPRepository.GetSingle(o => o.UserId == currentUser.Id, o => o.OrderByDescending(o => o.CreatedAt));

            if (latestOTP != null)
            {
                if ((DateTime.Now - latestOTP.CreatedAt).TotalMinutes < 2)
                {
                    throw new CustomException($"Cannot send new OTP right now, please wait for {(120 - (DateTime.Now - latestOTP.CreatedAt).TotalSeconds).ToString("0.00")} second(s)");
                }
            }

            string newOTP = CreateNewOTPCode();
            var htmlBody = HTMLEmail.SendingOTPEmail(currentUser.Fullname, newOTP, model.Subject.ToLower());
            bool sendEmailSuccess = await _emailService.SendEmail(model.Email, model.Subject, htmlBody);
            if (!sendEmailSuccess)
            {
                throw new CustomException("Error in sending email");
            }
            Otp newOTPCode = new Otp()
            {
                Id = Guid.NewGuid().ToString(),
                Code = newOTP,
                UserId = currentUser.Id,
                CreatedAt = DateTime.Now,
                IsUsed = false,
            };

            await _unitOfWork.OTPRepository.Insert(newOTPCode);
            await _unitOfWork.SaveChangeAsync();
           
        }

        public async Task VerifyOTP(OTPVerifyRequestModel model)
        {
            User currentUser = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(model.Email));

            if (currentUser == null)
            {
                throw new CustomException("There is no account with this email");
            }

            var latestOTPList = await _unitOfWork.OTPRepository.Get(o => o.UserId == currentUser.Id, o => o.OrderByDescending(o => o.CreatedAt));
            var latestOTP = latestOTPList.FirstOrDefault();

            if (latestOTP != null)
            {
                if ((DateTime.Now - latestOTP.CreatedAt).TotalMinutes > 30 || latestOTP.IsUsed)
                {
                    throw new CustomException("The OTP is already used or expired");
                }

                if (latestOTP.Code.Equals(model.OTP))
                {
                    latestOTP.IsUsed = true;
                }
                else
                {
                    throw new CustomException("The OTP is incorrect");
                }

                _unitOfWork.OTPRepository.Update(latestOTP);
                await _unitOfWork.SaveChangeAsync();

                
            }
        }
    }
}
