namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.VnPayDTOs
{
    public class VnPaymentRequestModel
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RedirectUrl { get; set; }
    }
}
