using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Business.Utils.CustomDataAnnotation;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.UserDTOs.RequestModels
{
    public class UserEditRequestModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Fullname must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Fullname can only contain letters and spaces.")]
        public string Fullname { get; set; }      

        [StringLength(15)]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Phone number is not in correct format!")]
        public string PhoneNumber { get; set; }
    }
}
