using Endpoint.MVC.Dtos.Enums;

namespace Endpoint.MVC.Dtos.Booth;

public class BoothDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; }
    public int Wage { get; set; }

    public Medal Medal { get; set; }
    public string PictureName { get; set; } = null!;

}
