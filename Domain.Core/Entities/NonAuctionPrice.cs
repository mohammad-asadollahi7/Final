using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class NonAuctionPrice
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int Discount { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
