using Domain.Core.Dtos.Products;
using Domain.Core.Enums;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Core.Dtos.Product;

public class AuctionDetailsDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public string EnglishTitle { get; set; } = null!;

    public string Description { get; set; } = null!;
    public decimal MaxPrice { get; set; }
    public int CategoryId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal MinPrice { get; set; }
    public string BoothTitle { get; set; }
    public SellType SellType { get; set; }
    public IEnumerable<CustomAttributeDto> CustomAttributes { get; set; }

    public string PictureName { get; set; }
}
