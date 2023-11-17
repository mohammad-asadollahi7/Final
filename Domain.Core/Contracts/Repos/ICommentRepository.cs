using Domain.Core.Dtos.Comment;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.Repos;

public interface ICommentRepository
{
    Task<List<CommentDto>> GetAllComments(bool? isApproved, CancellationToken cancellationToken);
    Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken);
    Task<bool> IsExistById(int id, CancellationToken cancellationToken);
    Task<int> GetNumberOfCommentsForApprove(CancellationToken cancellationToken);

    Task<List<CommentDto>> GetCommentsByProductId(int productId,
                                    CancellationToken cancellationToken);
}
