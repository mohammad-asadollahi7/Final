
using Domain.Core.Dtos.Product;

namespace Domain.Core.Contracts.Repos;

public interface IProductDapperRepository
{
    Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(CancellationToken cancellationToken,
                                                    params int[] ids);
}
