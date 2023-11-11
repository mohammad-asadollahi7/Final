
using Domain.Core.Dtos;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Domain.Core.Contracts.Repos;

public interface IAccountRepository
{
    Task<IdentityResult> Register(ApplicationUser user,
                                  string password,
                                  Role role, 
                                  CancellationToken cancellationToken);

    Task<ApplicationUser?> GetApplicationUserByUsername(string username);

    Task<ApplicationUser?> GetApplicationUserByEmail(string email);

    Task<List<string>> GetRoleNamesByUser(ApplicationUser user);

    Task<bool> CheckPassword(ApplicationUser user,
                             string password);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken);

    Task DeleteUser(int userId, CancellationToken cancellationToken);

    Task<bool> IsUserExistById(int userId, CancellationToken cancellationToken);
}
