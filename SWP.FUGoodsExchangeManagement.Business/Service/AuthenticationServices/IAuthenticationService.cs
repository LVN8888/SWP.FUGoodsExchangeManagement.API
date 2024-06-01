using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices
{
    public interface IAuthenticationService
    {
        string GenerateJWT(User User);
        string decodeToken(string jwtToken, string nameClaim);
    }
}
