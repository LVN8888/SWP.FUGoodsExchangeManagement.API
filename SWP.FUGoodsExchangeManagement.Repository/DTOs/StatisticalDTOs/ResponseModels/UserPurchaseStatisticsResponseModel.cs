using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.StatisticalDTOs.ResponseModels
{
    public class UserPurchaseStatisticsResponseModel
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
