using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels
{
    public class ProductPostPaymentModel
    {
        public string paymentUrl { get; set; } = null!;
        public string paymentId { get; set; } = null!;
    }
}
