
using System.Reflection.Metadata.Ecma335;

namespace Domain.Core.Entities;

public class CategoryPicture
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int PictureId { get; set; }
    public Category Category { get; set; }
    public Picture Picture { get; set; }

}
