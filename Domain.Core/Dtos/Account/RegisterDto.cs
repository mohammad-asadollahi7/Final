using Domain.Core.Enums;

namespace Domain.Core.Dtos.Account;


public class RegisterDto
{
    public string FName { get; set; }

    public string LName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string Password { get; set; }


    public string ConfirmPassword { get; set; }


    public Role Role { get; set; }

}
