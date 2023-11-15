using Endpoint.MVC.Dtos.Pictures;

namespace Endpoint.MVC.Dtos.Products;

public class CreateNonAuctionProductDto
{
    public string PersianTitle { get; set; }

    public string EnglishTitle { get; set; }

    public string Description { get; set; }

    public int FirstQuantity { get; set; }

    public decimal FirstPrice { get; set; }

    public int FirstDiscount { get; set; }

    public int CategoryId { get; set; }

    public List<CustomAttributeDto> CustomAttributes { get; set; }

    public PictureDto PictureDto { get; set; }
}
