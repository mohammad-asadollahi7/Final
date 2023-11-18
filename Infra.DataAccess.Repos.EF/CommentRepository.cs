using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Comment;
using Domain.Core.Entities;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infra.DataAccess.Repos.EF;

public class CommentRepository : ICommentRepository
{
    private readonly FinalContext _context;

    public CommentRepository(FinalContext context) => _context = context;

    public async Task<List<CommentDto>> GetAllComments(bool? isApproved,
                                            CancellationToken cancellationToken)
    {
        return await _context.Comments.Where(c => c.IsApproved == isApproved)
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
        var comment = await _context.Comments.FirstAsync(c => c.Id == id, cancellationToken);
        comment.IsApproved = isApproved;
        var f = await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistById(int id, CancellationToken cancellationToken)
    {
        return await _context.Comments.AnyAsync(c => c.Id == id);
    }

    public async Task<int> GetNumberOfCommentsForApprove(CancellationToken cancellationToken)
    {
        return await _context.Comments.Where(c => c.IsApproved == null).CountAsync(cancellationToken);
    }

    public async Task<List<CommentDto>> GetCommentsByProductId(int productId,
                                            CancellationToken cancellationToken)
    {
        return await _context.Comments.Where(c => c.ProductId == productId 
                                       && c.IsApproved == true)
                                       .Select(c => new CommentDto()
                                       {
                                           Id = c.Id,
                                           Description = c.Description,
                                           SubmittedDate = c.SubmittedDate,
                                           CustomerName = c.Customer.ApplicationUser.FullName,
                                           IsRecommended = c.IsRecommended,
                                           ProductTitle = c.Product.PersianTitle,
                                           Title = c.Title

                                       }).ToListAsync(cancellationToken);
    }

    public async Task Create(CreateCommentDto commentDto, 
                          CancellationToken cancellationToken)
    {
        var comment = new Comment()
        {
            IsApproved = null,
            CustomerId = commentDto.CustomerId,
            Description = commentDto.Description,
            SubmittedDate = commentDto.SubmittedDate,
            IsRecommended = commentDto.IsRecommended,
            Title = commentDto.Title,
            ProductId = commentDto.ProductId,
        };
        await _context.Comments.AddAsync(comment);  
        await _context.SaveChangesAsync(cancellationToken); 
    }
}

