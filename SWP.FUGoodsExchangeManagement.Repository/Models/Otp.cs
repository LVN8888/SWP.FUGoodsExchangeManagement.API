using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class Otp
{
    public string Id { get; set; } = null!;

    public string Otp1 { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsUsed { get; set; }

    public virtual User User { get; set; } = null!;
}
