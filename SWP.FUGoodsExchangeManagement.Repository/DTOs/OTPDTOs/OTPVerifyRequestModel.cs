using SWP.FUGoodsExchangeManagement.Business.Utils.CustomDataAnnotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.OTPDTOs
{
    public class OTPVerifyRequestModel
    {
        [FPTEmail]
        public string Email { get; set; } = null!;
        public string OTP { get; set; } = null!;
    }
}
