
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.AppServices;

public interface IBoothAppService
{
    Task<BoothDto> GetBySellerId(int sellerId, CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title,
                                            CancellationToken cancellationToken);

    Task Create(CreateBoothDto createBooth, int sellerId,
                CancellationToken cancellationToken);

    Task Update(int boothId, UpdateBoothDto boothDto,
                    CancellationToken cancellationToken);

    Task Delete(int boothId, CancellationToken cancellationToken);
    Task<BoothDto> GetById(int boothId, 
                           CancellationToken cancellationToken);
    Task<BoothDto> GetByTitle(string title,
                              CancellationToken cancellationToken);


}
