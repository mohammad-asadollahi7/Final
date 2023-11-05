
using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Exceptions;

namespace Domain.Services;

public class SellerService : ISellerService
{
    private readonly ISellerRepository _sellerRepository;

    public SellerService(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }
    public async Task AddByApplicationUserId(int userId, CancellationToken cancellationToken)
    {
        await _sellerRepository.AddByApplicationUserId(userId, cancellationToken);
    }

    public async Task<int> GetSellerIdByUserId(int userId, CancellationToken cancellationToken)
    {
        var sellerId = await _sellerRepository.GetSellerIdByUserId(userId, cancellationToken);
        if (sellerId == null)
            throw new AppException(ExpMessage.NotFoundUserId,
                                   ExpStatusCode.NotFound);
        return sellerId ?? 0;
    }
}
