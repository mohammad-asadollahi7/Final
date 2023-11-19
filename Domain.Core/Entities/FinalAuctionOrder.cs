
namespace Domain.Core.Entities;

public class FinalAuctionOrder
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
}
