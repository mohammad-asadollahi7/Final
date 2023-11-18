using Domain.Core.Enums;

namespace Domain.Core.Dtos.Account;


public class LoginDto
{

    public string Username { get; set; }


    public string Password { get; set; }


    public Role Role { get; set; }


}
