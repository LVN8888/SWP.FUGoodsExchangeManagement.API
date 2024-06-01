using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs
{
    public class UserLoginResponseModel
    {
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
