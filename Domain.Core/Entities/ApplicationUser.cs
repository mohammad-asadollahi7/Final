
using Microsoft.AspNetCore.Identity;

namespace Domain.Core.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public int Id { get; set; }
    public Admin Admin { get; set; }
    public Seller Seller { get; set; }
    public Customer Customer { get; set; }

}
