using Endpoint.MVC.Dtos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos;


public class LoginDto
{

    [Required(ErrorMessage = "فیلد الزامی")]
    [MinLength(5, ErrorMessage = "حداقل کاراکتر مورد نیاز 3")]
    public string Username { get; set; }



    [Required(ErrorMessage = "فیلد الزامی")]
    public string Password { get; set; }



    [Required(ErrorMessage = "فیلد الزامی")]
    public Role Role { get; set; }

}
