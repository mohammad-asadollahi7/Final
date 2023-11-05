
using Domain.Core.Contracts.Services;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.AppServices;

public interface IAccountAppService
{

    Task<string> Login(string username,
                       string password,
                       Role role,
                       CancellationToken cancellationToken);

    Task Register(ApplicationUser user,
                  string password,
                  Role role,
                  CancellationToken cancellationToken);

}
