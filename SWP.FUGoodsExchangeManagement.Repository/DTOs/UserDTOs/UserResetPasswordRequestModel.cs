using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs
{
    public class UserResetPasswordRequestModel
    {
        public Guid UserId { get; set; }
        public string currentPassword { get; set; } = null!;
        public string newPassword { get; set; } = null!;
        public string cfNewPassword { get; set; } = null!;
    }
}
