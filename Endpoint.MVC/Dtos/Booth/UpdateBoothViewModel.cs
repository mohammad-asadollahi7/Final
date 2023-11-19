using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Booth;

public class UpdateBoothViewModel
{
    public int Id { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Title { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Description { get; set; }

    public IFormFile? Picture { get; set; }
}
