namespace Endpoint.MVC.Dtos.Cart;

public class OrderWithProductDto
{
    public int Quantity { get; set; }

    public decimal DiscountedPrice { get; set; }

    public int ProductId { get; set; }
    public int BoothId { get; set; }
    public int Wage { get; set; }

}
