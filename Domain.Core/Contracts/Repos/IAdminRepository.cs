
using Domain.Core.Dtos.Account;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core.Contracts.Repos;


public interface IAdminRepository
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);


    Task<int?> GetAdminIdByUserId(int userId,
                                  CancellationToken cancellationToken);


    Task Update(int id, UpdateUserDto updateDto,
                        CancellationToken cancellationToken);

    Task<bool> IsExistById(int id, CancellationToken cancellationToken);

    Task<UserOutputDto?> Get(int id,
                      CancellationToken cancellationToken);


}
