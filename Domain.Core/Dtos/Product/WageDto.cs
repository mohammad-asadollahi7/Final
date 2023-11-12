

namespace Domain.Core.Dtos.Product;

public class WageDto
{
    public int Id { get; set; }
    public decimal Wages { get; set; }
    public DateTime Date { get; set; }
    public string ProductTitle { get; set; }
    public string BoothTitle { get; set; }
}

