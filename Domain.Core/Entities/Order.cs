using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal DiscountedPrice { get; set; }

    public int CartId { get; set; }

    public int BuyType { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
