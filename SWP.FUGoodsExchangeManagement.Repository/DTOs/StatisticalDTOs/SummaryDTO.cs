using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.StatisticalDTOs
{
    public class SummaryDTO
    {
        public int TotalUsers { get; set; }
        public int TotalPosts { get; set; }
        public int TotalReports { get; set; }
        public int TotalCampuses { get; set; }
        public int TotalCategories { get; set; }
    }
}
