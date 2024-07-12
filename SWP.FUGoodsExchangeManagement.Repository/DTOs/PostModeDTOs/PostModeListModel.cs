using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs
{
    public class PostModeListModel
    {
        public string Id { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Duration { get; set; } = null!;

        public string Price { get; set; } = null!;
    }
}
