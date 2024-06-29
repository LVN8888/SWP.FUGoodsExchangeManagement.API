using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.ResponseModels
{
    public class NewRefreshTokenResponseModel
    {
        public string accessToken { get; set; } = null!;
        public string refreshToken { get; set; } = null!;
    }
}
