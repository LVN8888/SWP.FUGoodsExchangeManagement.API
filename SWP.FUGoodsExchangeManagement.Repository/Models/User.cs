using System;
using System.Collections.Generic;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ICollection<PostApply> PostApplies { get; set; } = new List<PostApply>();

    public virtual ICollection<ProductPost> ProductPosts { get; set; } = new List<ProductPost>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
