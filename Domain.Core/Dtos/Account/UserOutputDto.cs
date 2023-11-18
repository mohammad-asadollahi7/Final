using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Dtos.Account;

public class UserOutputDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }

    public Role Role { get; set; }

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
