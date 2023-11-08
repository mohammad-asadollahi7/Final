using Domain.Core.Dtos.Comment;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.Repos;

public interface ICommentRepository
{
    Task<List<CommentDto>> GetCommentsForApprove(CancellationToken cancellationToken);
}
