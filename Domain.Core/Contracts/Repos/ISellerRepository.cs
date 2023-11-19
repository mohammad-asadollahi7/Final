
using Domain.Core.Dtos.Account;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core.Contracts.Repos;

public interface ISellerRepository
{
    Task AddByApplicationUserId(int userId,
                               CancellationToken cancellationToken);


    Task<int?> GetSellerIdByUserId(int userId,
                                  CancellationToken cancellationToken);
    Task DeleteSellerByUserId(int userId, CancellationToken cancellationToken);

    Task Update(int id, UpdateUserDto updateDto, CancellationToken cancellationToken);

     Task<bool> IsExixtById(int id, CancellationToken cancellationToken);

     Task<UserOutputDto?> Get(int id, CancellationToken cancellationToken);
   

}
