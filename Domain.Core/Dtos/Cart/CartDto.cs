
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Cart;

public class CartDto
{
    public int Id { get; set; }

    public CartStatus CartStatus { get; set; }

    public DateTime OrderAt { get; set; }

}
