
using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Domain.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository) => _customerRepository
                                                                           = customerRepository;
    public async Task AddByApplicationUserId(int applicationUserId,
                                             CancellationToken cancellationToken)
    {
        await _customerRepository.AddByApplicationUserId(applicationUserId, cancellationToken);
    }

    public async Task DeleteCustomerByUserId(int userId,
                                        CancellationToken cancellationToken)
    {
        await _customerRepository.DeleteCustomerByUserId(userId, cancellationToken);
    }

    public async Task<Customer> GetCustomerByPhoneNumber(string PhoneNumber, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByPhoneNumber(PhoneNumber, cancellationToken);
        if (customer is null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);
        return customer;
    }

    public async Task<int> GetCustomerIdByUserId(int applicationUserId,
                                                 CancellationToken cancellationToken)
    {
        var customerId = await _customerRepository.GetCustomerIdByUserId(applicationUserId, cancellationToken);
        if (customerId == null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);

        return customerId ?? 0;
    }

    public async Task Update(int id, UpdateUserDto updateDto,
                        CancellationToken cancellationToken)
    {
        await _customerRepository.Update(id, updateDto, cancellationToken);
    }

    public async Task EnsureExistById(int id, CancellationToken cancellationToken)
    {
        var isExist = await _customerRepository.IsExistById(id, cancellationToken);

        if (!isExist)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);
    }


    public async Task<UserOutputDto> Get(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.Get(id, cancellationToken);

        if(customer is null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);

        return customer;    
    }

   
}
