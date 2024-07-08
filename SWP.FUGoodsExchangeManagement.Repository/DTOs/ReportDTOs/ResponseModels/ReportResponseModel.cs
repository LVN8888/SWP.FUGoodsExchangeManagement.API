using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ReportDTOs.ResponseModels
{
    public class ReportResponseModel
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string ProductPostId { get; set; }
        public string CreatedBy { get; set; }
    }
}
