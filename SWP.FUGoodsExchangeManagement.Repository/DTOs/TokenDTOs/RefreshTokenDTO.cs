using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.TokenDTOs
{
    public class RefreshTokenDTO
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpiredDate { get; set; }
    }
}
