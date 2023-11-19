using Endpoint.MVC.Dtos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos;


public class RegisterDto
{
    [Required(ErrorMessage = "فیلد الزامی")]
    [MinLength(3, ErrorMessage = "حداقل کاراکتر مورد نیاز 3")]
    public string FName { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [MinLength(3, ErrorMessage = "حداقل کاراکتر مورد نیاز 3")]
    public string LName { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [MinLength(5, ErrorMessage = "حداقل کاراکتر مورد نیاز 3")]
    public string Username { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [EmailAddress(ErrorMessage = "فرمت نادرست")]
    public string Email { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [MinLength(11, ErrorMessage = "تعداد ارقام نادرست"), MaxLength(11, ErrorMessage = "تعداد ارقام نادرست")]
    public string PhoneNumber { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Password { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [Compare(nameof(Password), ErrorMessage = "عدم تطابق رمزعبور با تکرار رمزعبور")]
    public string ConfirmPassword { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public Role Role { get; set; }
}
