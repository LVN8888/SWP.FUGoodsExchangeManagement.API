using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class Report
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Date { get; set; }

    public string ProductPostId { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ProductPost ProductPost { get; set; } = null!;
}
