using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class ProductPost
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? ExpiredDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string PostModeId { get; set; } = null!;

    public string CampusId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string Price { get; set; } = null!;

    public virtual Campus Campus { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PostApply> PostApplies { get; set; } = new List<PostApply>();

    public virtual PostMode PostMode { get; set; } = null!;

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
