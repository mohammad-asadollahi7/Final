
namespace Domain.Core.Contracts.Services;

public interface IAdminService
{
    Task AddByApplicationUserId(int userId,
                                CancellationToken cancellationToken);
    Task<int> GetAdminIdByUserId(int userId,
                                 CancellationToken cancellationToken);
}
