using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.PostModeDTOs
{
    public class PostModeUpdateModel
    {
        [Required(ErrorMessage = "Id can't be null")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Please input type")]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Please input duration")]
        public string Duration { get; set; } = null!;

        [Required(ErrorMessage = "Please input price")]
        public string Price { get; set; } = null!;
    }
}
