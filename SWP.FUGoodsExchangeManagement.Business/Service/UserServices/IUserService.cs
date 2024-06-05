using SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs;
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
    }
}
