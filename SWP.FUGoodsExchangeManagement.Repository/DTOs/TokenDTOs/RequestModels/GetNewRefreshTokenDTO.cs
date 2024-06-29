using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs.RequestModels
{
    public class GetNewRefreshTokenDTO
    {
        public string refreshToken { get; set; } = null!;
    }
}
