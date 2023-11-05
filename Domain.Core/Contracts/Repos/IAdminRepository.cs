
namespace Domain.Core.Contracts.Repos;


public interface IAdminRepository
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);


    Task<int?> GetAdminIdByUserId(int userId,
                                  CancellationToken cancellationToken);
    
}
