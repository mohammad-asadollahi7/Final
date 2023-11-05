
namespace Domain.Core.Dtos.Product;

public class ProductOutputDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public decimal Price { get; set; }

    public int DiscountPercent { get; set; }

    public string MainPicturePath { get; set; }

}
