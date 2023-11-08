
using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Comment;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;

public class CommentRepository : ICommentRepository
{
    private readonly FinalContext _context;

    public CommentRepository(FinalContext context) => _context = context;

    public async Task<List<CommentDto>> GetCommentsForApprove(CancellationToken cancellationToken)
    {
        return await _context.Comments.Where(c => c.IsApproved == null)
                                      .Select(c => new CommentDto()
                                      {
                                          Id = c.Id,
                                          Title = c.Title,
                                          Description = c.Description,
                                          IsRecommended = c.IsRecommended,
                                          SubmittedDate = c.SubmittedDate,
                                          CustomerName = c.Customer.ApplicationUser.FullName,
                                          ProductTitle = c.Product.PersianTitle,
                                      }).AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task ApproveComment(int id, bool isApproved, 
                                CancellationToken cancellationToken)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id,
                                                                cancellationToken);
        comment.IsApproved = isApproved;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistById(int id, CancellationToken cancellationToken)
    {
        return await _context.Comments.AnyAsync(c => c.Id == id);
    }
}
