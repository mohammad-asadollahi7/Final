using Domain.Core.Dtos.Comment;

namespace Domain.Core.Contracts.Services;

public interface ICommentService
{
   Task<List<CommentDto>> GetCommentsForApprove(CancellationToken cancellationToken);
}
