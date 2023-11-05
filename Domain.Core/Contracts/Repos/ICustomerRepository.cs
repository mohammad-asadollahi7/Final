
using Domain.Core.Entities;

namespace Domain.Core.Contracts.Repos;

public interface ICustomerRepository 
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);

    Task<int?> GetCustomerIdByUserId(int applicationUserId,
                                     CancellationToken cancellationToken);
}
