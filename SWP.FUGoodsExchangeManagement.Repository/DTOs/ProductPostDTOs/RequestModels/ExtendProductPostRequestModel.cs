using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.RequestModels
{
    public class ExtendProductPostRequestModel
    {
        public string PostModeId { get; set; } = null!;
        public string RedirectUrl { get; set; } = null!;
    }
}
