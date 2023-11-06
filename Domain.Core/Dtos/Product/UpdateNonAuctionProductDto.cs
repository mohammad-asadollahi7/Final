
namespace Domain.Core.Dtos.Products;

public class UpdateNonAuctionProductDto
{
    public string PersianTitle { get; set; }

    public string EnglishTitle { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public string Description { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }

}
