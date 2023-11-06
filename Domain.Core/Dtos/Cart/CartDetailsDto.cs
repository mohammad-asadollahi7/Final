
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Cart;

public class CartDetailsDto
{
    public int Id { get; set; }

    public CartStatus CartStatus { get; set; }

    public DateTime OrderAt { get; set; }

    public int CustomerId { get; set; }

    public IEnumerable<OrderDto> OrderDtos { get; set; } = new List<OrderDto>();
}

