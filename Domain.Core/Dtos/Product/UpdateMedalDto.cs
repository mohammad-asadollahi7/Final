

using Domain.Core.Enums;

namespace Domain.Core.Dtos.Product;

public class UpdateMedalDto
{
    public int BoothId { get; set; }
    public decimal Wage { get; set; }
    public Medal Medal { get; set; }
}
