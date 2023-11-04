
using Domain.Core.Dtos.Product;

namespace Domain.Core.Contracts.Repos;

public interface IProductRepository
{
    Task<ProductDetailsDto?> GetNonAuctionsById(int productId,
                                                CancellationToken cancellationToken);
}
