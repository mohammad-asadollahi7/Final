using Domain.Core.Entities;

namespace Domain.Core.Dtos.Booth;

public class BoothDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; }
    public int Wage { get; set; }

    public string Medal { get; set; } = null!;
    public virtual Picture Picture { get; set; } = null!;

}
