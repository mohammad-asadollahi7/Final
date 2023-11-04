
namespace Domain.Core.Dtos.Products;

public class UpdateProductDto
{
    public string PersianTitle { get; set; }

    public string EnglishTitle { get; set; }

    public string Description { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }
}
