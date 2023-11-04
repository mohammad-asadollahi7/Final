
using Domain.Core.Dtos.Product;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Repos;

public interface IProductRepository
{
    Task<ProductDetailsDto?> GetNonAuctionsById(int productId,
                                                CancellationToken cancellationToken);

    Task<int> Create(string persianTitle,
                     string englishTitle,
                     string description,
                     int categoryId,
                     int boothId,
                     SellType sellType,
                     bool isCommit,
                     CancellationToken cancellationToken);
}
