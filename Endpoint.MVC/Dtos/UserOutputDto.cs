using Endpoint.MVC.Dtos.Enums;

namespace Endpoint.MVC.Dtos;


public class UserOutputDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public Role Role{ get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
