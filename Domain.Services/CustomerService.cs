
using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Exceptions;

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

    public async Task<int> GetCustomerIdByUserId(int applicationUserId,
                                                 CancellationToken cancellationToken)
    {
        var customerId = await _customerRepository.GetCustomerIdByUserId(applicationUserId, cancellationToken);
        if (customerId == null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);

        return customerId ?? 0;
    }
}
