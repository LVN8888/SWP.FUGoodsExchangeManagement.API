using SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Response
{
    public class PaymentResponseModel
    {
        public string Id { get; set; } = null!;

        public string? TransactionId { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Price { get; set; } = null!;

        public string Status { get; set; } = null!;

        public virtual PostModeListModel PostMode { get; set; } = null!;
    }
}
