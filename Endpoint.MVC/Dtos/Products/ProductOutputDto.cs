using Endpoint.MVC.Dtos.Enums;

namespace Endpoint.MVC.Dtos.Products;

public class ProductOutputDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } 

    public decimal Price { get; set; }
    public SellType SellType { get; set; }
    public int DiscountPercent { get; set; }
    public string BoothTitle { get; set; }
    public string PicturesName { get; set; }

}
