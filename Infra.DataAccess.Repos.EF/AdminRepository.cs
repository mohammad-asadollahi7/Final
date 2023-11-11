using Domain.Core.Contracts.Repos;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;


public class AdminRepository : IAdminRepository
{
    private readonly FinalContext _context;

    public AdminRepository(FinalContext context)
    {
        _context = context;
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
}
