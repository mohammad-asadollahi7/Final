
using Domain.Core.Dtos.Account;

namespace Domain.Core.Contracts.Services;

public interface IAdminService
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);
    Task<int> GetAdminIdByUserId(int userId,
                                 CancellationToken cancellationToken);

    Task Update(int id, UpdateUserDto updateDto,
                      CancellationToken cancellationToken);

    Task EnsureExistById(int id, CancellationToken cancellationToken);

     Task<UserOutputDto> Get(int id,
                            CancellationToken cancellationToken);
}
