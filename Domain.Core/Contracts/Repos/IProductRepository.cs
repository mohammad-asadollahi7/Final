
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
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

    Task AddCustomAttributes(int productId,
                                          List<CustomAttributeDto> attributes,
                                          bool isCommit,
                                          CancellationToken cancellationToken);
    Task AddNonAuctionPrice(decimal price,
                                         int discount,
                                         int productId,
                                         bool isCommit,
                                         CancellationToken cancellationToken);
    Task AddAuction(int productId,
                    DateTime fromDate,
                    DateTime toDate,
                    decimal minPrice,
                    bool isCommit,
                    CancellationToken cancellationToken);
}
