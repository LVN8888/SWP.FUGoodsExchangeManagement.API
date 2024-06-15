using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs
{
    public class DeleteCampusDTO
    {
        [Required]
        public string Id { get; set; }
    }
}
