using Domain.Core.Dtos.Products;
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Product;

public class ProductDetailsDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string EnglishTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int DiscountPercent { get; set; }

    public int BoothId { get; set; }

    public IEnumerable<CustomAttributeDto> CustomAttributes { get; set; }

    public IEnumerable<ProductPictureDto> ProductPictureDto { get; set; }
}
