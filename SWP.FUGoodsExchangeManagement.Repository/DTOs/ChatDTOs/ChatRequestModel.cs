using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ChatDTOs
{
    public class ChatRequestModel
    {
        public string Id { get; set; }
        public string ProductPostId { get; set; }
        public string BuyerId { get; set; }
        public List<ChatDetailRequestModel> ChatDetails { get; set; }
    }
}
