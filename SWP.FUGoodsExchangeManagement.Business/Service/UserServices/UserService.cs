using AutoMapper;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices;
using SWP.FUGoodsExchangeManagement.Business.Service.MailServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWP.FUGoodsExchangeManagement.Repository.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMailService _mailService;

        private const int UserPerPage = 10;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationService authenticationService, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _mailService = mailService;
        }

        public async Task<UserLoginResponseModel> CheckLogin(UserLoginRequestModel request)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));

            if (user == null)
            {
                throw new CustomException("User email not exist!");
            }

            if (!PasswordHasher.VerifyPassword(request.Password, user.Salt, user.Password))
            {
                throw new CustomException("Password incorrect!");
            }

            return new UserLoginResponseModel()
            {
                UserInfo = new UserInfo
                {
                    Fullname = user.Fullname,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role
                },
                token = _authenticationService.GenerateJWT(user)
            };
        }

        public async Task Register(UserRegisterRequestModel request)
        {
            User currentUser = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));
            if (currentUser != null)
            {
                throw new CustomException("User email existed!");
            }

            if (!request.Email.EndsWith("@fpt.edu.vn") && !request.Email.EndsWith("@fe.edu.vn"))
            {
                throw new CustomException("Email is not in correct format. Please input @fpt email!");
            }

            if (request.Role.IsNullOrEmpty())
            {
                throw new CustomException("Please enter role");
            }

            if (!Enum.IsDefined(typeof(RoleEnums), request.Role))
            {
                throw new CustomException("Please enter role in correct format");
            }

            if (!Regex.Match(request.PhoneNumber, @"^\d{10,11}$").Success)
            {
                throw new CustomException("Phone number is not in correct format!");
            }

            User newUser = _mapper.Map<User>(request);
            newUser.Id = Guid.NewGuid().ToString();
            var (salt, hash) = PasswordHasher.HashPassword(request.Password);
            newUser.Password = hash;
            newUser.Salt = salt;
            newUser.Status = AccountStatusEnums.Active.ToString();

            await _unitOfWork.UserRepository.Insert(newUser);
            var result = await _unitOfWork.SaveChangeAsync();

            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task RegisterAccount(UserRegisterRequestModelVer1 request)
        {
            User currentUser = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));

            if (currentUser != null)
            {
                throw new CustomException("User email existed!");
            }

            if (!request.Email.EndsWith("@fpt.edu.vn") && !request.Email.EndsWith("@fe.edu.vn"))
            {
                throw new CustomException("Email is not in correct format. Please input @fpt email!");
            }

            if (!Regex.Match(request.PhoneNumber, @"^\d{10,11}$").Success)
            {
                throw new CustomException("Phone number is not in correct format!");
            }

            if (request.Fullname.IsNullOrEmpty())
            {
                throw new CustomException("Please enter your name");
            }

            User newUser = _mapper.Map<User>(request);
            newUser.Id = Guid.NewGuid().ToString();
            newUser.Role = nameof(RoleEnums.User);
            newUser.Status = AccountStatusEnums.Active.ToString();
            var firstPassword = PasswordHasher.GenerateRandomPassword();
            var htmlBody = $"<h1>Your login information is:</h1><br/><p>Email: {request.Email}<br/>Password: {firstPassword}</p>";
            bool sendEmailSuccess = await _mailService.SendEmail(request.Email, "Login Information", htmlBody);

            if (!sendEmailSuccess)
            {
                throw new CustomException("Error in sending email");
            }

            var (salt, hash) = PasswordHasher.HashPassword(firstPassword);
            newUser.Password = hash;
            newUser.Salt = salt;

            await _unitOfWork.UserRepository.Insert(newUser);
            var result = await _unitOfWork.SaveChangeAsync();

            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task ResetPassword(UserResetPasswordRequestModel request)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));
            if (user == null)
            {
                throw new CustomException("There is no account using this email!");
            }

            if (!request.NewPassword.Equals(request.ConfirmPassword))
            {
                throw new CustomException("Password and comfirm password must match");
            }

            var (salt, hash) = PasswordHasher.HashPassword(request.NewPassword);
            user.Password = hash;
            user.Salt = salt;

            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task ChangePassword(UserChangePasswordRequestModel model, string token)
        {
            var userId = _authenticationService.decodeToken(token, "userId");
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(userId));
            if (user == null)
            {
                throw new CustomException("There is no account using this email!");
            }

            if (!PasswordHasher.VerifyPassword(model.OldPassword, user.Salt, user.Password))
            {
                throw new CustomException("Password incorrect!");
            }

            if (!model.NewPassword.Equals(model.ConfirmPassword))
            {
                throw new CustomException("Password and comfirm password must match");
            }

            if (PasswordHasher.VerifyPassword(model.NewPassword, user.Salt, user.Password))
            {
                throw new CustomException("New password must not match old password");
            }

            var (salt, hash) = PasswordHasher.HashPassword(model.NewPassword);
            user.Password = hash;
            user.Salt = salt;

            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task<List<UserListResponseModel>> GetUserList(int? page)
        {
            var qr = await _unitOfWork.UserRepository.Get(pageIndex: page,
                                                          pageSize: UserPerPage);
            return _mapper.Map<List<UserListResponseModel>>(qr);
        }
    }
}
