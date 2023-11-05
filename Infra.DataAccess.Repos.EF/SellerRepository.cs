using Domain.Core.Contracts.Repos;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;

public class SellerRepository : ISellerRepository
{
    private readonly FinalContext _context;

    public SellerRepository(FinalContext context)
    {
        _context = context;
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

    public async Task<int?> GetSellerIdByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _context.Sellers.Where(a => a.ApplicationUserId == userId)
                                           .Select(a => a.Id).FirstOrDefaultAsync(cancellationToken);
    }
}
