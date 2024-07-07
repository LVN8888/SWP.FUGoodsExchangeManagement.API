using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class PostApply
{
    public string Id { get; set; } = null!;

    public string? Message { get; set; }

    public string ProductPostId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public virtual User Buyer { get; set; } = null!;

    public virtual ProductPost ProductPost { get; set; } = null!;
}
