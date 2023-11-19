using Endpoint.MVC.Dtos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos;


public class UserOutputDto
{
    public int Id { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string FullName { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Username { get; set; }


    public Role Role{ get; set; }



    [Required(ErrorMessage = "فیلد الزامی")]
    [EmailAddress(ErrorMessage = "فرمت نادرست")]
    public string Email { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [MinLength(11, ErrorMessage = "تعداد ارقام نادرست"), MaxLength(11, ErrorMessage = "تعداد ارقام نادرست")]
    public string PhoneNumber { get; set; }
}
