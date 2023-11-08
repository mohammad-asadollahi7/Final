

namespace Domain.Core.Entities;

public class BoothPicture
{
    public int Id { get; set; }
    public int BoothId { get; set; }
    public int PictureId { get; set; }
    public Picture Picture { get; set; }
    public Booth Booth { get; set; }
}
