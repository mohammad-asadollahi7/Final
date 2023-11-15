using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Services;

public interface IBoothService
{
    Task<BoothDto> GetBoothBySellerId(int sellerId, CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title, 
                                            CancellationToken cancellationToken);

    Task Create(CreateBoothDto boothDto, int sellerId,
                CancellationToken cancellationToken);

    Task Update(int boothId, UpdateBoothDto boothDto,
                    CancellationToken cancellationToken);

    Task UpdateWage(int boothId,  CancellationToken cancellationToken);

    Task Delete(int boothId, CancellationToken cancellationToken);

    Task EnsureExistById(int id, CancellationToken cancellationToken);

    Task EnsureExistByTitle(string title, 
                                CancellationToken cancellationToken);
     Task<BoothDto> GetById(int boothId, 
                                CancellationToken cancellationToken);
     Task<BoothDto> GetByTitle(string title,
                               CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsBySellerId(int id,
                                            CancellationToken cancellationToken);

    Task EnsureExistBySellerId(int sellerId,
                              CancellationToken cancellationToken);
}
