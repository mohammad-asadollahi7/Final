using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Account;
using Domain.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository) => _adminRepository = adminRepository;


    public async Task AddByApplicationUserId(int userId, 
                                            CancellationToken cancellationToken)
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

    public async Task Update(int id, UpdateUserDto updateDto,
                       CancellationToken cancellationToken)
    {
        await _adminRepository.Update(id, updateDto, cancellationToken);
    }

    public async Task EnsureExistById(int id, CancellationToken cancellationToken)
    {
        var isExist = await _adminRepository.IsExistById(id, cancellationToken);

        if (!isExist)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);
    }


    public async Task<UserOutputDto> Get(int id,
                                     CancellationToken cancellationToken)
    {
        var admin = await _adminRepository.Get(id, cancellationToken);

        if(admin is null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);

        return admin;
    }

}
