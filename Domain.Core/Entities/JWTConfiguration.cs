
namespace Domain.Core.Entities;

public class JWTConfiguration
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string IssuerSigningKey { get; set; }
}
