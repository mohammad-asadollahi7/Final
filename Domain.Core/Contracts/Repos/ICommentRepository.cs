using Domain.Core.Dtos.Comment;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.Repos;

public interface ICommentRepository
{
    Task<List<CommentDto>> GetCommentsForApprove(bool? isApproved, CancellationToken cancellationToken);
    Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken);
    Task<bool> IsExistById(int id, CancellationToken cancellationToken);
}
