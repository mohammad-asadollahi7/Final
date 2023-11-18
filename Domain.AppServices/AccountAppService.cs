using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Domain.Core.Enums;
using System.Data;

namespace Domain.AppServices;

public class AccountAppService : IAccountAppService
{
    private readonly IAccountService _accountService;
    public AccountAppService(IAccountService accountService) =>
                                _accountService = accountService;

    public async Task<LoginOutputDto> Login(string username,
                                            string password,
                                            Role role,
                                            CancellationToken cancellationToken)
    {
        _accountService.EnsureRoleExist(role);
        var user = await _accountService.GetApplicationUserByUsername(username);
        await _accountService.EnsurePassword(user, password);
        var roleNames = await _accountService.GetRoleNamesByUser(user);
        _accountService.EnsureRoleValidity(role.ToString(), roleNames.First());
        var JWTToken = await _accountService.GenerateJWTToken(user,
                                                              roleNames.First(),
                                                              cancellationToken);
        var loginOutput = new LoginOutputDto()
        {
            FullName = user.FullName,
            Token = JWTToken,
        };
        return loginOutput;
    }

    public async Task Register(ApplicationUser user,
                               string password,
                               Role role,
                               CancellationToken cancellationToken)
    {
        await _accountService.EnsureUniquePhoneNumber(user.PhoneNumber, cancellationToken);
        _accountService.EnsureRoleExist(role);
        await _accountService.Register(user, password, role, cancellationToken);
        var sumbittedApplicationUser = await _accountService.GetApplicationUserByEmail(user.Email);

        await _accountService.CreateRoleByUserId(sumbittedApplicationUser.Id,
                                                 role,
                                                 cancellationToken);

        await _accountService.SaveChangesAsync(cancellationToken);

    }


    public async Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken)
    {
        return await _accountService.GetUsers(cancellationToken);
    }

    public async Task DeleteUser(int userId, Role role,
                               CancellationToken cancellationToken)
    {
        await _accountService.EnsureUserExist(userId, cancellationToken);
        await _accountService.DeleteUser(userId, role, cancellationToken);
    }

    public async Task<int> GetUserNumbers(CancellationToken cancellationToken)
    {
        return await _accountService.GetUserNumbers(cancellationToken);
    }
}
