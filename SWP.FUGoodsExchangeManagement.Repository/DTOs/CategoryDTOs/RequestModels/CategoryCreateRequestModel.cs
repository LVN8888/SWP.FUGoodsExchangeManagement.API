﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.CategoryDTOs.RequestModel
{
    public class CategoryCreateRequestModel
    {
        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; } = null!;
    }
}
