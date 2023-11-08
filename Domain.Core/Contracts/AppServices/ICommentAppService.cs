
using Domain.Core.Dtos.Comment;

namespace Domain.Core.Contracts.AppServices;

public interface ICommentAppService
{
    Task<List<CommentDto>> GetCommentsForApprove(CancellationToken cancellationToken);
    
}
