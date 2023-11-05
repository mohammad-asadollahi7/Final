using Endpoint.MVC.Dtos.Products;

namespace Endpoint.MVC.Dtos.Cart;

public class OrderDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal DiscountedPrice { get; set; }

    public ProductInOrderDto ProductDto { get; set; } = null!;
}
