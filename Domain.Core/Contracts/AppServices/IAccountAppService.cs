using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.AppServices;

public interface IAccountAppService
{

    Task<LoginOutputDto> Login(string username,
                              string password,
                              Role role,
                              CancellationToken cancellationToken);

    Task Register(ApplicationUser user,
                  string password,
                  Role role,
                  CancellationToken cancellationToken);

    Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken);

    Task DeleteUser(int userId, Role role,
                      CancellationToken cancellationToken);

    Task<int> GetUserNumbers(CancellationToken cancellationToken);


}
