using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel
{
    public class CategoryUpdateRequestModel
    {
        [Required(ErrorMessage = "Id is missing")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } = null!;
    }
}
