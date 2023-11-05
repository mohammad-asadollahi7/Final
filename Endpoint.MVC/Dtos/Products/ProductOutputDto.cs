namespace Endpoint.MVC.Dtos.Products;

public class ProductOutputDto
{
    public int Id { get; set; }

    public string PersianTitle { get; set; } = null!;

    public double CalculatedRate { get; set; }

    public decimal Price { get; set; }

    public int DiscountPercent { get; set; }

    public string MainPicturePath { get; set; }

}
