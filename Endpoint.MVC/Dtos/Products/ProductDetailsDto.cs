using Endpoint.MVC.Dtos.Pictures;

namespace Endpoint.MVC.Dtos.Products;


public class ProductDetailsDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string EnglishTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? DiscountPercent { get; set; }
    public int CategoryId { get; set; }

    public decimal Price { get; set; }
    public string BoothTitle { get; set; }

    public List<CustomAttributeDto> CustomAttributes { get; set; }

    public List<ProductPictureDto> ProductPictureDto { get; set; }

}
