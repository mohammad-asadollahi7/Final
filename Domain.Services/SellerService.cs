
using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Account;
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

    public async Task DeleteSellerByUserId(int userId, CancellationToken cancellationToken)
    {
        await _sellerRepository.DeleteSellerByUserId(userId, cancellationToken);
    }

    public async Task Update(int id, UpdateUserDto updateDto, 
                                CancellationToken cancellationToken)
    {
        await _sellerRepository.Update(id, updateDto, cancellationToken);   
    }

    public async Task EnsureExistById(int id, CancellationToken cancellationToken)
    {
        var isExist = await _sellerRepository.IsExixtById(id, cancellationToken);

        if(!isExist)
            throw new AppException(ExpMessage.NotFoundUserId,
                                 ExpStatusCode.NotFound);
    }

    public async Task<UserOutputDto> Get(int id, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.Get(id, cancellationToken);

        if(seller is null)
                throw new AppException(ExpMessage.NotFoundUserId,
                                     ExpStatusCode.NotFound);

        return seller;
    }
}
