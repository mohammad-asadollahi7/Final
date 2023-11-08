using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Comment;
using Domain.Core.Exceptions;

namespace Domain.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<List<CommentDto>> GetCommentsForApprove(CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetCommentsForApprove(cancellationToken);
        if (comments.Count() == 0)
            throw new AppException(ExpMessage.HaveNotComment, ExpStatusCode.NotFound);

        return comments;    
    }
}
