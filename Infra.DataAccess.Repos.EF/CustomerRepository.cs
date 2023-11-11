
using Domain.Core.Contracts.Repos;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;

public class CustomerRepository : ICustomerRepository
{
    private readonly FinalContext _context;

    public CustomerRepository(FinalContext context) => _context = context;


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
}

