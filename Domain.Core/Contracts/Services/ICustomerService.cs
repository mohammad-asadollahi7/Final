using Domain.Core.Dtos.Account;
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

    Task Update(int id, UpdateUserDto updateDto,
                        CancellationToken cancellationToken);

    Task EnsureExistById(int id, CancellationToken cancellationToken);

    Task<UserOutputDto> Get(int id, CancellationToken cancellationToken);


}
