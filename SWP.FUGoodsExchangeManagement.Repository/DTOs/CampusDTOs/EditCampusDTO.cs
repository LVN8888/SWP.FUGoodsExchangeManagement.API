using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs
{
    public class EditCampusDTO
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Campus name is required")]
        [StringLength(100, ErrorMessage = "Campus name cannot be longer than 100 characters")]
        public string Name { get; set; }
    }
}
