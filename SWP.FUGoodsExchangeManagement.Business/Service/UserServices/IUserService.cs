using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.ResponseModels;

namespace SWP.FUGoodsExchangeManagement.Business.Service.UserServices
{
    public interface IUserService
    {
        Task<UserLoginResponseModel> CheckLogin(UserLoginRequestModel requestModel);
        Task Register(UserRegisterRequestModel requestModel);
        Task RegisterAccount(UserRegisterRequestModelVer1 request);
        Task ResetPassword(UserResetPasswordRequestModel request);
        Task ChangePassword(UserChangePasswordRequestModel model, string token);
        Task<List<UserListResponseModel>> GetUserList(int? page, string? search, string? sort, string? userRole);
        Task<NewRefreshTokenResponseModel> GetNewRefreshToken(GetNewRefreshTokenDTO newRefreshToken);
        Task ActivateUser(string userId);
        Task DeactivateUser(string userId);
        Task EditUser(UserEditRequestModel request);
        Task ChangeUserRole(UserRoleChangeRequestModel request);
        Task Logout(GetNewRefreshTokenDTO newRefreshToken);
        Task<UserResponseModel> GetDetailsOfUser(string id);
    }
}
