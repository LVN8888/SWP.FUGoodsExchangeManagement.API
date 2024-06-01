using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class Chat
{
    public string Id { get; set; } = null!;

    public string ProductPostId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<ChatDetail> ChatDetails { get; set; } = new List<ChatDetail>();

    public virtual ProductPost ProductPost { get; set; } = null!;
}
