using AutoMapper;
using SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<UserLoginResponseModel> CheckLogin(UserLoginRequestModel request)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));

            if (user == null)
            {
                throw new Exception("User email not exist!");
            }

            if (!PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                throw new Exception("Password incorrect!");
            }

            return new UserLoginResponseModel()
            {
                Token = _authenticationService.GenerateJWT(user),
                Role = user.Role
            };
        }

        public async Task Register(UserRegisterRequestModel request)
        {
            User currentUser = await _unitOfWork.UserRepository.GetSingle(u => u.Email.Equals(request.Email));
            if (currentUser != null)
            {
                throw new Exception("User email existed!");
            }

            User newUser = _mapper.Map<User>(request);
            newUser.Id = Guid.NewGuid().ToString();
            newUser.Role = nameof(RoleEnums.User);
            newUser.Password = PasswordHasher.HashPassword(request.Password);

            await _unitOfWork.UserRepository.Insert(newUser);
            var result = await _unitOfWork.SaveChangeAsync();

            if (result < 1)
            {
                throw new Exception("Internal Server Error");
            }
        }
    }
}
