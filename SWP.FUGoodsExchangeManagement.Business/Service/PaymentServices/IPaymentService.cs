using SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Request;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.PaymentServices
{
    public interface IPaymentService
    {
        Task<Payment> UpdatePaymentStatus(PaymentUpdateRequestModel model);
    }
}
