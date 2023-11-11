using Domain.Core.Entities;

namespace Domain.Core.Contracts.Services;

public interface ICustomerService
{
    Task AddByApplicationUserId(int applicationUserId,
                                CancellationToken cancellationToken);

    Task<int> GetCustomerIdByUserId(int applicationUserId,
                                    CancellationToken cancellationToken);

    Task DeleteCustomerByUserId(int userId, 
                                CancellationToken cancellationToken);

    Task<Customer> GetCustomerByPhoneNumber(string PhoneNumber,
                                             CancellationToken cancellationToken);
}
