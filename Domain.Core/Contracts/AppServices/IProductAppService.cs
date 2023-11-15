using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Exceptions;

namespace Domain.Core.Contracts.AppServices;

public interface IProductAppService
{
    Task<int> GetWageNumbers(CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(int categoryId,
                                                    CancellationToken cancellationToken);

    Task<AuctionDetailsDto> GetAuctionProductById(int productId, bool? isApproved,
                                                   CancellationToken cancellationToken);

    Task<ProductDetailsDto> GetNonAuctionProductById(int productId, bool? isApproved,
                                                     CancellationToken cancellationToken);

    Task<List<ProductOutputApprove>> GetProductsForApprove(
                                                CancellationToken cancellationToken);

    Task CreateNonAuction(int sellerId, CreateNonAuctionProductDto createProduct,
                                      CancellationToken cancellationToken);

    Task CreateAuction(int selleraId, CreateAuctionProductDto createProduct,
                       CancellationToken cancellationToken);


    Task UpdateNonAuction(int productId,
                         UpdateNonAuctionProductDto productDto,
                         CancellationToken cancellationToken);

    Task UpdateAuction(int productId, AuctionDetailsDto productDto,
                             CancellationToken cancellationToken);

    Task Remove(int productId,
                CancellationToken cancellationToken);

    Task ApproveProduct(int id, bool isApproved,
                        CancellationToken cancellationToken);

    Task<List<WageDto>> GetWages(CancellationToken cancellationToken);

    Task<int> GetNumberOfProductsForApprove(CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetAuctions(bool? isApproved,
                                            CancellationToken cancellationToken);



}
