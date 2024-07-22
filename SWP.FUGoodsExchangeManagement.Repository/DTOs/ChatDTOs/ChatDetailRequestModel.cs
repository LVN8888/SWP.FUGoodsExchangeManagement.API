using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ChatDTOs
{
    public class ChatDetailRequestModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public bool Flag { get; set; }
        public string ChatId { get; set; }
    }
}
