using Domain.Core.Dtos.Comment;

namespace Domain.Core.Contracts.Services;

public interface ICommentService
{
   Task<List<CommentDto>> GetCommentsForApprove(bool? isApproved, CancellationToken cancellationToken);
    Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken);
    Task EnsureExistById(int id, CancellationToken cancellationToken);


}
