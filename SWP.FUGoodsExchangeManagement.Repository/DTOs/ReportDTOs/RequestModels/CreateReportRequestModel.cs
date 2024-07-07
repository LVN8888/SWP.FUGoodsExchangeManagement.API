using SWP.FUGoodsExchangeManagement.Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ReportDTOs.RequestModels
{
    public class CreateReportRequestModel
    {
        public string ProductPostId { get; set; }
        public string CreatedBy { get; set; }
        public List<ReportReasonEnums> Reasons { get; set; }
    }
}
