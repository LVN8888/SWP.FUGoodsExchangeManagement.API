using SWP.FUGoodsExchangeManagement.Repository.DTOs.VnPayDTOs;
using Microsoft.AspNetCore.Http;

namespace SWP.FUGoodsExchangeManagement.Business.VnPayService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);

        VnPaymentResponseModel PaymentResponse(IQueryCollection colletions);
    }
}
