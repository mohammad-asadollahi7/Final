
namespace Domain.Core.Dtos.Product;

public class CreateWageDto
{
    public int BoothId { get; set; }
    public DateTime Date { get; set; }
    public int ProductId { get; set; }
    public decimal FinalWage { get; set; }

}
