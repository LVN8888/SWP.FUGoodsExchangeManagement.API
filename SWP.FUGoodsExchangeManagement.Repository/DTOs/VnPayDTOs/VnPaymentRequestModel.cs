namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.VnPayDTOs
{
    public class VnPaymentRequestModel
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
