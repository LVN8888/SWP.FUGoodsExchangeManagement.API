using SWP.FUGoodsExchangeManagement.Repository.DTOs.VnPayDTOs;

namespace SWP.FUGoodsExchangeManagement.API.VnPayService
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);

        VnPaymentResponseModel MakePayment(IQueryCollection colletions);
    }
}
