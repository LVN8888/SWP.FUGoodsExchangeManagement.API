using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class RefreshToken
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime ExpiredDate { get; set; }

    public virtual User User { get; set; } = null!;
}
