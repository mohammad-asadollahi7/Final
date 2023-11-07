
namespace Endpoint.MVC.Dtos.Products;
public class UpdateProductDto
{
    public int Id { get; set; }
    public string PersianTitle { get; set; }

    public string EnglishTitle { get; set; }

    public string Description { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }

}
