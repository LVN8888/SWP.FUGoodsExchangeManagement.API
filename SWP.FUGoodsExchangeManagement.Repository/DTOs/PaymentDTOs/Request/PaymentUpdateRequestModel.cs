using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Request
{
    public class PaymentUpdateRequestModel
    {
        public string Id { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string TransactionId { get; set; } = null!;
    }
}
