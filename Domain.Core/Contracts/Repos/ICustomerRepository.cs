
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core.Contracts.Repos;

public interface ICustomerRepository 
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);
    Task DeleteCustomerByUserId(int userId, CancellationToken cancellationToken);

    Task<int?> GetCustomerIdByUserId(int applicationUserId,
                                     CancellationToken cancellationToken);

    Task<Customer?> GetCustomerByPhoneNumber(string PhoneNumber,
                                             CancellationToken cancellationToken);
   
}
