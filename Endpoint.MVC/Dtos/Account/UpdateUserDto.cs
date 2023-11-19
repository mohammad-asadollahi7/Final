
using System.ComponentModel.DataAnnotations;

namespace Endpoint.MVC.Dtos.Account;
public class UpdateUserDto
{
    [Required(ErrorMessage = "فیلد الزامی")]
    public string FullName { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [EmailAddress(ErrorMessage = "فرمت نادرست")]
    public string Email { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string Username { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string PhoneNumber { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    public string OldPassword { get; set; }



    [Required(ErrorMessage = "فیلد الزامی")]
    public string NewPassword { get; set; }


    [Required(ErrorMessage = "فیلد الزامی")]
    [Compare(nameof(NewPassword), ErrorMessage = "رمزعبور نامنطبق")]
    public string ConfirmPassword { get; set; }


}
