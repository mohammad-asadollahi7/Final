using System.Reflection.Metadata.Ecma335;

namespace Endpoint.MVC.Dtos.Products;
public class UpdateProductDto
{
    public int Id { get; set; }
    public string PersianTitle { get; set; }

    public string EnglishTitle { get; set; }

    public string Description { get; set; }
    public  string BoothTitle { get; set; }
    public int CategoryId { get; set; }
    public int Discount { get; set; }
    public decimal Price { get; set; }
    public List<CustomAttributeDto> CustomAttributes { get; set; }

}
