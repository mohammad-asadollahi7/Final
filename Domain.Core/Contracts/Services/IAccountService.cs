using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace Domain.Core.Contracts.Services;

public interface IAccountService
{
    Task Register(ApplicationUser user,
                         string password,
                         Role role,
                         CancellationToken cancellationToken);
    Task EnsureUniquePhoneNumber(string phoneNumber,
                                 CancellationToken cancellationToken);

    Task<ApplicationUser> GetApplicationUserByEmail(string email);

    Task<ApplicationUser?> GetApplicationUserByUsername(string username);

    Task EnsurePassword(ApplicationUser user,
                        string password);

    Task CreateRoleByUserId(int userId, Role role,
                            CancellationToken cancellationToken);


    Task<string> GenerateJWTToken(ApplicationUser user,
                                  string role,
                                  CancellationToken cancellationToken);

    Task<List<string>> GetRoleNamesByUser(ApplicationUser user);

    void EnsureRoleValidity(string currentUserRoleName,
                            string databaseRoleName);

    Task SaveChangesAsync(CancellationToken cancellationToken);
    void EnsureRoleExist(Role role);
    Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken);
    Task DeleteUser(int userId, Role role, CancellationToken cancellationToken);
    Task EnsureUserExist(int userId, CancellationToken cancellationToken);

    Task<int> GetUserNumbers(CancellationToken cancellationToken);

    Task Update(int id, string role, UpdateUserDto updateDto,
                           CancellationToken cancellationToken);

     Task<UserOutputDto> Get(int id, string role,
                          CancellationToken cancellationToken);
}

