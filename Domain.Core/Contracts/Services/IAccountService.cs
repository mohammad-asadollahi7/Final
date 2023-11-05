
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Services;

public interface IAccountService
{
    Task Register(ApplicationUser user,
                  string password,
                  Role role);
    Task EnsureUniquePhoneNumber(string phoneNumber,
                                 CancellationToken cancellationToken);

    Task<ApplicationUser> GetApplicationUserByEmail(string email);

    Task<ApplicationUser?> GetApplicationUserByUsername(string username);

    Task EnsurePassword(ApplicationUser user,
                        string password);

    Task CreateRoleByUserId(int userId,
                                       Role role,
                                       CancellationToken cancellationToken);


    Task<string> GenerateJWTToken(ApplicationUser user,
                                  string role,
                                  CancellationToken cancellationToken);

    Task<List<string>> GetRoleNamesByUser(ApplicationUser user);

    void EnsureRoleValidity(string currentUserRoleName,
                            string databaseRoleName);
}
