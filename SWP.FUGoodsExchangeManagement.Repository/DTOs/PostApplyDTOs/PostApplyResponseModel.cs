using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.PostApplyDTOs
{
    public class PostApplyResponseModel
    {
        public string Id { get; set; } = null!;

        public string? Message { get; set; }

        public BuyerInfo BuyerInfo { get; set; } = null!;
    }

    public class BuyerInfo
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
