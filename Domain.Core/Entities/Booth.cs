
using Domain.Core.Enums;

namespace Domain.Core.Entities;

public partial class Booth
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; }
    public int Wage { get; set; }

    public Medal Medal { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Picture Picture { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public int SellerId { get; set; }

    public virtual Seller? Seller { get; set; }

    public virtual ICollection<ProductsBooth> ProductsBooths { get; set; } = new List<ProductsBooth>();
}