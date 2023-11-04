using System;
using System.Collections.Generic;

namespace Domain.Core.Entities;

public partial class Booth
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Wage { get; set; }

    public string Medal { get; set; } = null!;

    public virtual Picture IdNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<ProductsBooth> ProductsBooths { get; set; } = new List<ProductsBooth>();

    public virtual Seller? Seller { get; set; }
}
