using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Repos;

public interface IBoothRepository
{
    Task<BoothDto?> GetBySellerId(int sellerId, CancellationToken cancellationToken);

    Task<BoothDto?> GetById(int boothId, CancellationToken cancellationToken);
    Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title, 
                                          CancellationToken cancellationToken);

    Task<List<ProductInventoryDto>> GetInventoriesByBoothId(int boothId,
                                                            CancellationToken cancellationToken);

    Task Create(CreateBoothDto boothDto,
                int sellerId,
                int wage,
                Medal medal,
                bool isDeleted,
                CancellationToken cancellationToken);

    Task Update(int boothId, UpdateBoothDto boothDto, 
                    CancellationToken cancellationToken);

    Task UpdateWage(int boothId, int wage, 
                         Medal medal, CancellationToken cancellationToken);

    Task Delete(int boothId, CancellationToken cancellationToken);

    Task<bool> IsExistById(int id,
                           CancellationToken cancellationToken);
    Task<bool> IsExistBySellerId(int sellerId, CancellationToken cancellationToken);

    Task<bool> IsExistByTitle(string title,
                             CancellationToken cancellationToken);
    Task<BoothDto?> GetByTitle(string title, CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsBySellerId(int id, 
                            CancellationToken cancellationToken);
    Task<List<ProductOutputDto>> GetAuctionsBySellerId(int id, 
                               CancellationToken cancellationToken);
    Task DeleteProductsOfDeletedBooth(int boothId, CancellationToken cancellationToken);

}