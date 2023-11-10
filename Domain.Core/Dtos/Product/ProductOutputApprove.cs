
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Product;

public class ProductOutputApprove
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public SellType SellType { get; set; }
    public string BoothTitle { get; set; }

    public IEnumerable<string> PicturesPath { get; set; }
}
