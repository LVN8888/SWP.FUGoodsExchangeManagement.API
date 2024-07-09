using AutoMapper;
using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using MimeKit.Encodings;
using Org.BouncyCastle.Asn1.Ocsp;
using SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices;
using SWP.FUGoodsExchangeManagement.Business.Service.MailServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
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
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.ResponseModels;

namespace SWP.FUGoodsExchangeManagement.Business.Service.UserServices
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

        public async Task<NewRefreshTokenResponseModel> GetNewRefreshToken(GetNewRefreshTokenDTO newRefreshToken)
        {
            RefreshToken refreshToken = await _unitOfWork.TokenRepository.GetSingle(t => t.Token.Equals(newRefreshToken.refreshToken));
            if (refreshToken == null)
            {
                throw new CustomException("Refresh token not exist!");
            }
            User user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(refreshToken.UserId));
            var newAccessToken = _authenticationService.GenerateJWT(user);
            var updatedRefreshTokenDto = new NewRefreshTokenResponseModel
            {
                accessToken = newAccessToken.accessToken,
                refreshToken = newAccessToken.refreshToken
            };
            refreshToken.Token = newAccessToken.refreshToken;
            refreshToken.ExpiredDate = DateTime.Now.AddDays(2);
            RefreshToken newrefreshToken = _mapper.Map<RefreshToken>(refreshToken);
            _unitOfWork.TokenRepository.Update(newrefreshToken);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
            return updatedRefreshTokenDto;
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

            var tokens = _authenticationService.GenerateJWT(user);

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Token = tokens.refreshToken,
                ExpiredDate = DateTime.Now.AddDays(2)
            };

            await _unitOfWork.TokenRepository.Insert(refreshToken);

            var result = await _unitOfWork.SaveChangeAsync();

            if (result < 1)
            {
                throw new Exception("Internal Server Error");
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
                accessToken = tokens.accessToken,
                refreshToken = tokens.refreshToken
            };

        }


        public async Task Register(UserRegisterRequestModel request)
        {
            User currentUser = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));
            if (currentUser != null)
            {
                throw new CustomException("User email existed!");
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

        private Func<IQueryable<User>, IOrderedQueryable<User>> GetOrderQuery(string sort)
        {
            switch (sort)
            {
                case nameof(UserSortEnums.Name_Desc):
                    return u => u.OrderByDescending(u => u.Fullname);
                case nameof(UserSortEnums.Name_Asc):
                    return u => u.OrderBy(u => u.Fullname);
                default:
                    return u => u.OrderBy(u => u.Fullname);
            }
        }

        public async Task<List<UserListResponseModel>> GetUserList(int? page, string? search, string? sort, string? userRole)
        {
            if (!string.IsNullOrEmpty(sort) && !Enum.IsDefined(typeof(UserSortEnums), sort))
                throw new CustomException("Sort value is not valid");

            var searchUnsign = Util.ConvertToUnsign(search ?? "");

            var qr = await _unitOfWork.UserRepository.Get(pageIndex: page ?? 0,
                                                          pageSize: UserPerPage,
                                                          filter: u => (u.Fullname.Contains(searchUnsign) || u.Email.Contains(searchUnsign)) &&
                                                                        (string.IsNullOrEmpty(userRole) || u.Role.Equals(userRole))
                                                                  ,
                                                          orderBy: GetOrderQuery(sort)
                                                         );
            return _mapper.Map<List<UserListResponseModel>>(qr);
        }

        public async Task ActivateUser(string userId)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(userId));
            if (user == null)
            {
                throw new CustomException("User not found!");
            }
            user.Status = AccountStatusEnums.Active.ToString();
            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task DeactivateUser(string userId)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(userId));
            if (user == null)
            {
                throw new CustomException("User not found!");
            }
            user.Status = AccountStatusEnums.Inactive.ToString();
            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task EditUser(UserEditRequestModel request)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(request.Id));
            if (user == null)
            {
                throw new CustomException("User not found!");
            }           
            var existingUserWithPhoneNumber = await _unitOfWork.UserRepository.GetSingle(u => u.PhoneNumber.Equals(request.PhoneNumber) && !u.Id.Equals(request.Id));
            if (existingUserWithPhoneNumber != null)
            {
                throw new CustomException("Phone number already exists!");
            }          
            user.Fullname = request.Fullname;
            user.PhoneNumber = request.PhoneNumber;
            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task ChangeUserRole(UserRoleChangeRequestModel request)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(request.UserId));
            if (user == null)
            {
                throw new CustomException("User not found!");
            }

            if (!Enum.IsDefined(typeof(RoleEnums), request.NewRole))
            {
                throw new CustomException("Role is not valid.");
            }

            user.Role = request.NewRole;

            _unitOfWork.UserRepository.Update(user);
            var result = await _unitOfWork.SaveChangeAsync();
            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }

        public async Task Logout(GetNewRefreshTokenDTO newRefreshToken)
        {
            var refreshToken = await _unitOfWork.TokenRepository.GetSingle(t => t.Token.Equals(newRefreshToken.refreshToken));
            if (refreshToken != null)
            {
                _unitOfWork.TokenRepository.Delete(refreshToken);
                await _unitOfWork.SaveChangeAsync();

            }
        }

        public async Task<UserResponseModel> GetDetailsOfUser(string id)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Id.Equals(id));
            if (user == null)
            {
                throw new CustomException("User is not existed");
            }
            var response = _mapper.Map<UserResponseModel>(user);
            return response;
        }

    }
}
