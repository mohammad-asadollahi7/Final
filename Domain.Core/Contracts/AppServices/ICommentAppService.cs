
using Domain.Core.Dtos.Comment;

namespace Domain.Core.Contracts.AppServices;

public interface ICommentAppService
{
    Task<List<CommentDto>> GetAllComments(bool? isApproved, CancellationToken cancellationToken);

    Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken);

    Task<int> GetNumberOfCommentsForApprove(CancellationToken cancellationToken);

    Task<List<CommentDto>> GetCommentsByProductId(int productId,
                                            CancellationToken cancellationToken);

    Task Create(CreateCommentDto commentDto,
                CancellationToken cancellationToken);
}
