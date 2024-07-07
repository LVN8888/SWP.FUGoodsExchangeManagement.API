using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.RequestModels
{
    public class ProductPostAddModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime ExpiredDate { get; set; }

    }
}
