
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.Repos;

public interface IBoothDapperRepository
{

    Task UpdateMedal(List<UpdateMedalDto> updateMedalDtos,
                   CancellationToken cancellationToken);

    Task<List<ProductInventoryDto>> GetInventoriesByBoothIds(List<int> boothIds,
                                                      CancellationToken cancellationToken);
}
