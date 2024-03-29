﻿using Domain.Core.Contracts.Repos;
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
    public async Task<List<CommentDto>> GetAllComments(bool? isApproved, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetAllComments(isApproved, 
                                                            cancellationToken);
        if (comments.Count() == 0)
            throw new AppException(ExpMessage.HaveNotComment, ExpStatusCode.NotFound);

        return comments;    
    }

    public async Task ApproveComment(int id, bool isApproved, CancellationToken cancellationToken)
    {
        await _commentRepository.ApproveComment(id, isApproved, cancellationToken);   
    }

    public async Task EnsureExistById(int id, CancellationToken cancellationToken)
    {
        var isExist = await _commentRepository.IsExistById(id, cancellationToken);
        if (!isExist)
            throw new AppException(ExpMessage.HaveNotComment, ExpStatusCode.NotFound);
    }

    public async Task<int> GetNumberOfCommentsForApprove(CancellationToken cancellationToken)
    {
        return await _commentRepository.GetNumberOfCommentsForApprove(cancellationToken);
    }

    public async Task<List<CommentDto>> GetCommentsByProductId(int productId,
                                            CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetCommentsByProductId(productId, 
                                                                cancellationToken);
        if (comments.Count() == 0)
            throw new AppException(ExpMessage.HaveNotComment, ExpStatusCode.NotFound);

        return comments;
    }

    public async Task Create(CreateCommentDto commentDto,
                          CancellationToken cancellationToken)
    {
        commentDto.SubmittedDate = DateTime.Now;
        await _commentRepository.Create(commentDto, cancellationToken);
    }
}
