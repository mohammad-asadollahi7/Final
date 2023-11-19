
using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Account;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;

public class CustomerRepository : ICustomerRepository
{
    private readonly FinalContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomerRepository(FinalContext context,
                                UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task AddByApplicationUserId(int userId,
                                             CancellationToken cancellationToken)
    {
        var customer = new Customer()
        {
            ApplicationUserId = userId,
        };
        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCustomerByUserId(int userId, 
                                    CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FirstAsync(c => c.ApplicationUserId == userId,
                                                    cancellationToken);
        _context.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserOutputDto?> Get(int id, CancellationToken cancellationToken)
    {
       return await _context.Customers.Where(c => c.Id == id)
                               .Select(c => new UserOutputDto()
                                 {
                                     Id = c.Id,
                                     Email = c.ApplicationUser.Email,
                                     FullName = c.ApplicationUser.FullName,
                                     PhoneNumber = c.ApplicationUser.PhoneNumber,
                                     Username = c.ApplicationUser.UserName,
                                 }).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<Customer?> GetCustomerByPhoneNumber(string PhoneNumber, CancellationToken cancellationToken)
    {
        return await _context.Customers.FirstOrDefaultAsync(c =>
                                c.ApplicationUser.PhoneNumber == PhoneNumber, cancellationToken);
    }

    public async Task<int?> GetCustomerIdByUserId(int applicationUserId,
                                                  CancellationToken cancellationToken)
    {
        return await _context.Customers.Where(c => c.ApplicationUserId == applicationUserId)
                                       .Select(c => c.Id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> IsExistById(int id, CancellationToken cancellationToken)
    {
        return _userManager.Users.AnyAsync(u => u.Customer.Id == id, cancellationToken);
    }

    public async Task Update(int id, UpdateUserDto updateDto, 
                        CancellationToken cancellationToken)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Customer.Id == id, cancellationToken);
        user.PhoneNumber = updateDto.PhoneNumber;
        user.UserName = updateDto.Username;
        user.FullName = updateDto.FullName;
        user.Email = updateDto.Email;
        await _userManager.ChangePasswordAsync(user, updateDto.OldPassword,
                                               updateDto.NewPassword);
    }

   

}

