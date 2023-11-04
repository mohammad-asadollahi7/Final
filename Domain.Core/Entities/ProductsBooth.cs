using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class ProductsBooth
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int BoothId { get; set; }

    /// <summary>
    /// 0 equal NonAuction
    /// 1 equal Auction
    /// 
    /// </summary>
    public bool Type { get; set; }

    public virtual Booth Booth { get; set; } = null!;
}
