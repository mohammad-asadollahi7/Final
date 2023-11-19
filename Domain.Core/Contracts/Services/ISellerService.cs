
using Domain.Core.Dtos.Account;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core.Contracts.Services;

public interface ISellerService
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);
    Task<int> GetSellerIdByUserId(int userId,
                                  CancellationToken cancellationToken);
    Task DeleteSellerByUserId(int userId, CancellationToken cancellationToken);
    Task Update(int id, UpdateUserDto updateDto,
                  CancellationToken cancellationToken);
    Task EnsureExistById(int id, CancellationToken cancellationToken);

    Task<UserOutputDto> Get(int id, CancellationToken cancellationToken);
   

}
