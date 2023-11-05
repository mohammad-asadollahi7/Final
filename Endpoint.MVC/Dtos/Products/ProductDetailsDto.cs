using Endpoint.MVC.Dtos.Pictures;

namespace Endpoint.MVC.Dtos.Products;


public class ProductDetailsDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string EnglishTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double? CalculatedRate { get; set; }

    public int DiscountPercent { get; set; }

    public decimal Price { get; set; }

    public List<CustomAttributeDto> CustomAttributes { get; set; }

    public List<ProductPictureDto> ProductPictureDto { get; set; }

}
