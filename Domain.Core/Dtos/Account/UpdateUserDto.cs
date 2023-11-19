
namespace Domain.Core.Dtos.Account;

public class UpdateUserDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

}
