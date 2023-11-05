using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Exceptions;

namespace Domain.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository) => _adminRepository = adminRepository;


    public async Task AddByApplicationUserId(int userId, CancellationToken cancellationToken)
    {
        await _adminRepository.AddByApplicationUserId(userId, cancellationToken);
    }

    public async Task<int> GetAdminIdByUserId(int userId, CancellationToken cancellationToken)
    {
        var adminId = await _adminRepository.GetAdminIdByUserId(userId, cancellationToken);
        if (adminId == null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);
        return adminId ?? 0;
    }

}
