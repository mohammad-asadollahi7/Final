using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class ProductInventory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public bool IsSold { get; set; }

    public DateTime ChangedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
