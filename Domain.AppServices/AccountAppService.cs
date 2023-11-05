using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Entities;
using Domain.Core.Enums;
using System.Data;

namespace Domain.AppServices;

public class AccountAppService : IAccountAppService
{
    private readonly IAccountService _accountService;


    public AccountAppService(IAccountService accountService) => _accountService = accountService;


    public async Task<string> Login(string username,
                                    string password,
                                    Role role,
                                    CancellationToken cancellationToken)
    {
        var user = await _accountService.GetApplicationUserByUsername(username);
        await _accountService.EnsurePassword(user, password);
        var roleNames = await _accountService.GetRoleNamesByUser(user);
        _accountService.EnsureRoleValidity(role.ToString(), roleNames.First());
        var JWTToken = await _accountService.GenerateJWTToken(user,
                                                              roleNames.First(),
                                                              cancellationToken);
        return JWTToken;
    }



    public async Task Register(ApplicationUser user,
                               string password,
                               Role role,
                               CancellationToken cancellationToken)
    {
        await _accountService.EnsureUniquePhoneNumber(user.PhoneNumber, cancellationToken);
        await _accountService.Register(user, password, role);
        var sumbittedApplicationUser = await _accountService.GetApplicationUserByEmail(user.Email);
        await _accountService.CreateAdminOrCustomerByUserId(sumbittedApplicationUser.Id,
                                                            role,
                                                            cancellationToken);

    }
}
