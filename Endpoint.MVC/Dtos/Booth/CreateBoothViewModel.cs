
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Booth;

public class CreateBoothViewModel
{
    [Required(ErrorMessage = "فیلد الزامی")]
    public string Title { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Description { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public IFormFile Picture { get; set; }
}
