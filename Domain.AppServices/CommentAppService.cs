
using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Comment;

namespace Domain.AppServices;

public class CommentAppService : ICommentAppService
{
    private readonly ICommentService _commentService;
    private readonly ICartService _cartService;

    public CommentAppService(ICommentService commentService, ICartService cartService)
    {
        _commentService = commentService;
        _cartService = cartService;
    }

    public async Task<List<CommentDto>> GetAllComments(bool? isApproved, CancellationToken cancellationToken)
    {
        return await _commentService.GetAllComments(isApproved, cancellationToken);
    }

    public async Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken)
    {
        await _commentService.EnsureExistById(id, cancellationToken);
        await _commentService.ApproveComment(id, isApproved, cancellationToken);
    }

    public async Task<int> GetNumberOfCommentsForApprove(CancellationToken cancellationToken)
    {
        return await _commentService.GetNumberOfCommentsForApprove(cancellationToken);
    }

    public async Task<List<CommentDto>> GetCommentsByProductId(int productId,
                                            CancellationToken cancellationToken)
    {
        return await _commentService.GetCommentsByProductId(productId, cancellationToken);
    }

    public async Task Create(CreateCommentDto commentDto,
                        CancellationToken cancellationToken)
    {
        await _cartService.EnsureCustomerBuyed(commentDto.CustomerId,
                                            commentDto.ProductId, cancellationToken);
        await _commentService.Create(commentDto, cancellationToken);
    }
}
