
using System.Reflection.Metadata.Ecma335;

namespace Domain.Core.Entities;

public class Wage
{
    public int Id { get; set; }
    public decimal Wages { get; set; }
    public DateTime Date { get; set; }
    public int BoothId { get; set; }
    public int ProductId { get; set; }
    public Booth Booth { get; set; }
    public Product Product { get; set; }
}
