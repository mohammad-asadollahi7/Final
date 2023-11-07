using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class AuctionOrder
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual Product Product { get; set; } = null!;
}
