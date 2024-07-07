using SWP.FUGoodsExchangeManagement.Business.Utils.CustomDataAnnotation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels
{
    public class UserResetPasswordRequestModel
    {
        [Required(ErrorMessage = "Please input email")]
        [FPTEmail]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Please input new password")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Please input confirm password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
