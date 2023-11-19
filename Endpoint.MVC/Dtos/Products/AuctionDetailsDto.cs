using Endpoint.MVC.Dtos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Products;

public class AuctionDetailsDto
{
    public int Id { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string PersianTitle { get; set; } 


    [Required(ErrorMessage = "فیلد الزامی")]
    public string EnglishTitle { get; set; } 


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Description { get; set; } 

    public decimal MaxPrice { get; set; }

    public int CategoryId { get; set; }

    public decimal MinPrice { get; set; }

    public string BoothTitle { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public SellType SellType { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public List<CustomAttributeDto> CustomAttributes { get; set; }

    public string PictureName { get; set; }
}
