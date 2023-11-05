
namespace Domain.Core.Contracts.Repos;

public interface ISellerRepository
{
    Task AddByApplicationUserId(int userId,
                               CancellationToken cancellationToken);


    Task<int?> GetSellerIdByUserId(int userId,
                                  CancellationToken cancellationToken);
}
