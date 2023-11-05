namespace Domain.Core.Contracts.Services;

public interface ICustomerService
{
    Task AddByApplicationUserId(int applicationUserId,
                                CancellationToken cancellationToken);

    Task<int> GetCustomerIdByUserId(int applicationUserId,
                                    CancellationToken cancellationToken);
}
