using Endpoint.MVC.Dtos.Enums;

namespace Endpoint.MVC.Dtos.Products;

public class CreateAuctionViewModel
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string EnglishTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CategoryId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal MinPrice { get; set; }
    public string BoothTitle { get; set; }
    public SellType SellType { get; set; }

    public IEnumerable<CustomAttributeDto> CustomAttributes { get; set; }

    public IFormFile Picture { get; set; }
}
