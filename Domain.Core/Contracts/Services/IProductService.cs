
using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Services;

public interface IProductService
{
    Task<ProductDetailsDto?> GetNonAuctionProductById(int productId,
                                                     CancellationToken cancellationToken);

    Task<ProductDetailsDto?> GetAuctionProductById(int productId,
                                                   CancellationToken cancellationToken);
    Task<List<ProductOutputDto>> GetAllByCategoryId(CancellationToken cancellationToken,
                                                    params int[] ids);
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

    Task AddPicture(int productId,
                    PictureDto pictureDto,
                    CancellationToken canellationToken,
                    bool isCommit);



    Task AddQuantityRecord(int productId,
                           int quantity,
                           DateTime submitDate,
                           bool isSold,
                           CancellationToken cancellationToken,
                           bool isCommit);

    Task Update(int productId,
                UpdateProductDto productDto,
                CancellationToken cancellationToken);

    Task UpdateAuctionRecord(int productId,
                             AuctionDto auctionDto,
                             bool isCommit,
                             CancellationToken cancellationToken);


    Task UpdateNonAuctionPrice(int productId,
                               decimal price,
                               int discount,
                               bool isCommit,
                               CancellationToken cancellationToken);



    Task Remove(int productId,
                CancellationToken cancellationToken);


    Task EnsureExistById(int productId,
                         CancellationToken cancellationToken);

    Task EnsureProductQuantitySufficient(int productId,
                                         int quantity,
                                         CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
