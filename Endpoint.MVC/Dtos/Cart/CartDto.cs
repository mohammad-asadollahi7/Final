using Endpoint.MVC.Dtos.Enums;

namespace Endpoint.MVC.Dtos.Cart;

public class CartDto
{
    public int Id { get; set; }

    public CartStatus CartStatus { get; set; }

    public DateTime OrderAt { get; set; }

}


