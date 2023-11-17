
namespace Domain.Core.Dtos.Product;

public class ProductInventoryDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public bool IsSold { get; set; }
    public decimal? SellPrice { get; set; }
    public int BoothId { get; set; }

}
