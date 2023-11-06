
namespace Domain.Core.Dtos.Categories;

public class CategoryDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int? ParentId { get; set; }

    public string PictureName { get; set; }

    public IEnumerable<SubCategoryDto>? SubCategories { get; set; }

    public IEnumerable<int>? ProductIds { get; set; }

    public IEnumerable<string>? AttributeTitles { get; set; }

}
