
using Domain.Core.Dtos.Comment;

namespace Domain.Core.Contracts.AppServices;

public interface ICommentAppService
{
    Task<List<CommentDto>> GetCommentsForApprove(bool? isApproved, CancellationToken cancellationToken);

    Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken);


}
