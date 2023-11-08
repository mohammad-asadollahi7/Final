
using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Comment;

namespace Domain.AppServices;

public class CommentAppService : ICommentAppService
{
    private readonly ICommentService _commentService;

    public CommentAppService(ICommentService commentService)
    {
        _commentService = commentService;
    }
    public async Task<List<CommentDto>> GetCommentsForApprove(CancellationToken cancellationToken)
    {
        return await _commentService.GetCommentsForApprove(cancellationToken);
    }
}
