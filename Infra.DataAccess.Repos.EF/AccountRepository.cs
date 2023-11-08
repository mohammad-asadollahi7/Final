using Domain.Core.Contracts.Repos;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infra.Db.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;

namespace Infra.DataAccess.Repos.EF;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly FinalContext _context;

    public AccountRepository(UserManager<ApplicationUser> userManager,
                             FinalContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IdentityResult> Register(ApplicationUser user,
                                               string password, Role role,
                                               CancellationToken cancellationToken)
    {
        var identityResult = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, role.ToString());
        return identityResult;
    }


    public async Task<ApplicationUser?> GetApplicationUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser?> GetApplicationUserByUsername(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<bool> CheckPassword(ApplicationUser user,
                                          string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<List<string>> GetRoleNamesByUser(ApplicationUser user)
    {
        var roleNames = await _userManager.GetRolesAsync(user);
        return roleNames.ToList();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    

}
