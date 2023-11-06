
namespace Domain.Core.Dtos.Products;

public class UpdateAuctionProductDto
{
    public string PersianTitle { get; set; }
    public string EnglishTitle { get; set; }
    public string Description { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public decimal MinPrice { get; set; }

    public bool IsActive { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }

}
