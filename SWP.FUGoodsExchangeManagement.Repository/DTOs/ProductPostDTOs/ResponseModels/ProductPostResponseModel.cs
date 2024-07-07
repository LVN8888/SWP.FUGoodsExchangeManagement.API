using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels
{
    public class ProductPostResponseModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Price { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public PostAuthor CreatedBy { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string PostMode { get; set; } = null!;

        public string Campus { get; set; } = null!;

        public List<string> ImageUrls { get; set; } = null!;
    }

    public class PostSearchModel
    {
        public string? Title { get; set; }

        public string? Category { get; set; }

        public string? Campus { get; set; }

        public bool? orderPriceDescending { get; set; }

        public bool? orderDateDescending { get; set; }
    }

    public class PostAuthor
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
