using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class ChatDetail
{
    public string Id { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime Time { get; set; }

    public bool Flag { get; set; }

    public string ChatId { get; set; } = null!;

    public virtual Chat Chat { get; set; } = null!;
}
