using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;


public class AdminRepository : IAdminRepository
{
    private readonly FinalContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminRepository(FinalContext context, 
                            UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    public async Task AddByApplicationUserId(int userId,
                                            CancellationToken cancellationToken)
    {
        var newAdmin = new Admin()
        {
            ApplicationUserId = userId,
        };

        await _context.Admins.AddAsync(newAdmin, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int?> GetAdminIdByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _context.Admins.Where(a => a.ApplicationUserId == userId)
                                 .Select(a => a.Id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> IsExistById(int id, CancellationToken cancellationToken)
    {
       return _userManager.Users.AnyAsync(u => u.Admin.Id == id, cancellationToken);
    }

    public async Task Update(int id, UpdateUserDto updateDto, 
                        CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Admin.Id == id, cancellationToken);
        user.PhoneNumber = updateDto.PhoneNumber;
        user.UserName = updateDto.Username;
        user.FullName = updateDto.FullName;
        user.Email = updateDto.Email;
        await _userManager.ChangePasswordAsync(user,updateDto.OldPassword,
                                               updateDto.NewPassword);
    }

    public async Task<UserOutputDto?> Get(int id, 
                                         CancellationToken cancellationToken)
    {
        return await _context.Admins.Where(c => c.Id == id)
                                .Select(c => new UserOutputDto()
                                {
                                    Id = c.Id,
                                    Email = c.ApplicationUser.Email,
                                    FullName = c.ApplicationUser.FullName,
                                    PhoneNumber = c.ApplicationUser.PhoneNumber,
                                    Username = c.ApplicationUser.UserName,
                                }).SingleOrDefaultAsync(cancellationToken);
    }

}
