using Domain.Core.Enums;

namespace Endpoint.API.Dtos;


public class LoginDto
{

    public string Username { get; set; }


    public string Password { get; set; }


    public Role Role { get; set; }


}
