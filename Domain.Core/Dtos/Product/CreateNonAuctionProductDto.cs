using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Product;

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
