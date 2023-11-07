

using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Exceptions;

namespace Domain.Core.Contracts.AppServices;

public interface IProductAppService
{
    Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(int categoryId,
                                                    CancellationToken cancellationToken);

    Task<ProductDetailsDto> GetAuctionProductById(int productId,
                                                   CancellationToken cancellationToken);

    Task<ProductDetailsDto> GetNonAuctionProductById(int productId,
                                                     CancellationToken cancellationToken);

    Task<List<ProductOutputApprove>> GetProductsForApprove(
                                                        CancellationToken cancellationToken);

    Task CreateNonAuction(CreateNonAuctionProductDto createProduct,
                           CancellationToken cancellationToken);

    Task CreateAuction(CreateAuctionProductDto createProduct,
                       CancellationToken cancellationToken);


    Task UpdateNonAuction(int productId,
                         UpdateNonAuctionProductDto productDto,
                         CancellationToken cancellationToken);

    Task UpdateAuction(int productId, UpdateAuctionProductDto productDto,
                             CancellationToken cancellationToken);

    Task Remove(int productId,
                CancellationToken cancellationToken);
}
