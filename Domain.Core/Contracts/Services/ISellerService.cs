
namespace Domain.Core.Contracts.Services;

public interface ISellerService
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);
    Task<int> GetSellerIdByUserId(int userId,
                                  CancellationToken cancellationToken);
    Task DeleteSellerByUserId(int userId, CancellationToken cancellationToken);

}
