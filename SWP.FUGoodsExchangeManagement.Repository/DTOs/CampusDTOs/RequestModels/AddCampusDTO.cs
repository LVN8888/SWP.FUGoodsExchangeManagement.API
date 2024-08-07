﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.CampusDTOs.RequestModels
{
    public class AddCampusDTO
    {
        [Required(ErrorMessage = "Campus name is required")]
        [StringLength(100, ErrorMessage = "Campus name cannot be longer than 100 characters")]
        public string Name { get; set; }
    }
}
