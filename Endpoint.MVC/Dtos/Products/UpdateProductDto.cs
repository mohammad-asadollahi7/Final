using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Endpoint.MVC.Dtos.Products;
public class UpdateProductDto
{
    public int Id { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string PersianTitle { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string EnglishTitle { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Description { get; set; }

    public string BoothTitle { get; set; }
    public int CategoryId { get; set; }

    public int Discount { get; set; }

    public decimal Price { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public List<CustomAttributeDto> CustomAttributes { get; set; }

}
