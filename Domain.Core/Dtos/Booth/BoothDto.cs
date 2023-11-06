using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Booth;

public class BoothDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; }
    public int Wage { get; set; }

    public Medal Medal { get; set; }
    public virtual Picture Picture { get; set; } = null!;

}
