using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class PostMode
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public string Price { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<ProductPost> ProductPosts { get; set; } = new List<ProductPost>();
}
