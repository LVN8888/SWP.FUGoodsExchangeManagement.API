using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs
{
    public class UserLoginRequestModel
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
