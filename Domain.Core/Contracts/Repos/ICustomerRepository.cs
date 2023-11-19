
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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

    Task Update(int id, UpdateUserDto updateDto, CancellationToken cancellationToken);

    Task<bool> IsExistById(int id, CancellationToken cancellationToken);

    Task<UserOutputDto?> Get(int id, CancellationToken cancellationToken);

}
