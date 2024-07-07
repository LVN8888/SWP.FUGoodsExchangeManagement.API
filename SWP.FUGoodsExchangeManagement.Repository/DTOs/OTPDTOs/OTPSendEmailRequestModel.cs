using SWP.FUGoodsExchangeManagement.Business.Utils.CustomDataAnnotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.OTPDTOs
{
    public class OTPSendEmailRequestModel
    {
        [FPTEmail]
        public string Email { get; set; } = null!;
        public string Subject { get; set; } = null!;
    }
}
