using Endpoint.MVC.Dtos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Products;

public class AuctionDetailsDto
{
    public int Id { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string PersianTitle { get; set; } = null!;

    [Required(ErrorMessage = "فیلد الزامی")]
    public string EnglishTitle { get; set; } = null!;


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Description { get; set; } = null!;

    public decimal MaxPrice { get; set; }

    public int CategoryId { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public decimal MinPrice { get; set; }

    public string BoothTitle { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public DateTime FromDate { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public DateTime ToDate { get; set; }

    public SellType SellType { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public List<CustomAttributeDto> CustomAttributes { get; set; }

    public string PictureName { get; set; }
}
