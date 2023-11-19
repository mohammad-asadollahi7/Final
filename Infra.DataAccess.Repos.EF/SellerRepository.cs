using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Infra.DataAccess.Repos.EF;

public class SellerRepository : ISellerRepository
{
    private readonly FinalContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SellerRepository(FinalContext context,
                            UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task AddByApplicationUserId(int userId, CancellationToken cancellationToken)
    {
        var newSeller = new Seller()
        {
            ApplicationUserId = userId,
        };

        await _context.Sellers.AddAsync(newSeller, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteSellerByUserId(int userId, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers.FirstAsync(s => s.ApplicationUserId == userId, 
                                                        cancellationToken);
        _context.Sellers.Remove(seller);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int?> GetSellerIdByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _context.Sellers.Where(a => a.ApplicationUserId == userId)
                                    .Select(a => a.Id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Update(int id, UpdateUserDto updateDto,
                                CancellationToken cancellationToken)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Seller.Id == id, cancellationToken);
        user.PhoneNumber = updateDto.PhoneNumber;
        user.UserName = updateDto.Username;
        user.FullName = updateDto.FullName;
        user.Email = updateDto.Email;
        await _userManager.ChangePasswordAsync(user, updateDto.OldPassword,
                                               updateDto.NewPassword);
    }

    public async Task<bool> IsExixtById(int id, CancellationToken cancellationToken)
    {
        return await _userManager.Users.AnyAsync(u => u.Seller.Id == id, cancellationToken);
    }


    public async Task<UserOutputDto?> Get(int id, CancellationToken cancellationToken)
    {
        return await _context.Sellers.Where(c => c.Id == id)
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
