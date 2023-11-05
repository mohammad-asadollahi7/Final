using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public int ApplicationUserId { get; set; }

    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual ICollection<AuctionOrder> AuctionOrders { get; set; } = new List<AuctionOrder>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
