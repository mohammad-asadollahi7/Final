using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Products;

public class CustomAttributeDto
{
    public int Id { get; set; }
    public string Title { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Value { get; set; }

}
