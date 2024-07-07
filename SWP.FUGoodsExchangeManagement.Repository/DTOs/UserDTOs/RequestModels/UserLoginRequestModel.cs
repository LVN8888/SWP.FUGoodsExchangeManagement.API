using SWP.FUGoodsExchangeManagement.Business.Utils.CustomDataAnnotation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels
{
    public class UserLoginRequestModel
    {
        [Required(ErrorMessage = "Please input email")]
        [FPTEmail]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please input password")]
        public string Password { get; set; } = null!;
    }
}
