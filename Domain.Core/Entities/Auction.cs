using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Auction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public decimal MinPrice { get; set; }

    public bool IsActive { get; set; }

    public virtual Product Product { get; set; } = null!;
}
