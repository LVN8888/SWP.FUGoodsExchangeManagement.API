﻿using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.Service.UserServices
{
    public interface IUserService
    {
        Task<UserLoginResponseModel> CheckLogin(UserLoginRequestModel requestModel);
        Task Register(UserRegisterRequestModel requestModel);
        Task RegisterAccount(UserRegisterRequestModelVer1 request);
        Task ResetPassword(UserResetPasswordRequestModel request);
        Task ChangePassword(UserChangePasswordRequestModel model, string token);
        Task<List<UserListResponseModel>> GetUserList(int? page, string? search, string? sort, string? userRole);
    }
}
