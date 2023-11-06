using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Repos;

public interface IBoothRepository
{
    Task<BoothDto?> GetBoothBySellerId(int sellerId, CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title, 
                                          CancellationToken cancellationToken);

    Task<List<ProductInventoryDto>> GetInventoriesByBoothId(int boothId,
                                                            CancellationToken cancellationToken);

    Task Create(int sellerId,
                string title,
                string description,
                int wage,
                Medal medal,
                Picture picture, 
                CancellationToken cancellationToken);

    Task Update(int boothId, UpdateBoothDto boothDto, 
                    CancellationToken cancellationToken);

    Task UpdateWage(int boothId, int wage, 
                         Medal medal, CancellationToken cancellationToken);

    Task Delete(int boothId, CancellationToken cancellationToken);

    Task<bool> IsExistById(int id,
                           CancellationToken cancellationToken);

    Task<bool> IsExistByTitle(string title,
                             CancellationToken cancellationToken);

}