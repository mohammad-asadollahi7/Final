﻿
using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Cart;
using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Services;

public interface IProductService
{
    Task<ProductDetailsDto?> GetNonAuctionProductById(int productId, bool? isApproved,
                                                     CancellationToken cancellationToken);

    Task<AuctionDetailsDto?> GetAuctionProductById(int productId, bool? isApproved,
                                                   CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(CancellationToken cancellationToken,
                                                    params int[] ids);

    Task<List<ProductOutputApprove>> GetProductsForApprove(CancellationToken cancellationToken);

    Task<SellType> GetSellType(int productId, CancellationToken cancellationToken);

    Task<int> Create(int sellerId,
                     string persianTitle,
                     string englishTitle,
                     string description,
                     int categoryId,
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

    Task AddQuantityRecord(int cartId,
                           DateTime submitDate,
                           bool isSold,
                           CancellationToken cancellationToken,
                           bool isCommit);

    Task UpdateAuctionProduct(int productId,
                               AuctionDetailsDto productDto,
                               bool isCommit,
                               CancellationToken cancellationToken);


    Task UpdateNonAuctionProduct(int productId,
                               UpdateNonAuctionProductDto productDto,
                               bool isCommit,
                               CancellationToken cancellationToken);


    Task Remove(int productId,
                CancellationToken cancellationToken);


    Task EnsureExistById(int productId, bool? isApproved,
                         SellType? sellType,
                         CancellationToken cancellationToken);

    Task EnsureProductQuantitySufficient(int productId,
                                         int quantity,
                                         CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task ApproveProduct(int id, bool isApproved,
                                CancellationToken cancellationToken);

    Task<List<WageDto>> GetWages(CancellationToken cancellationToken);

    Task<int> GetNumberOfProductsForApprove(CancellationToken cancellationToken);
    Task<int> GetWageNumbers(CancellationToken cancellationToken);
    Task<List<ProductOutputDto>> GetAuctions(bool? isApproved,
                                            CancellationToken cancellationToken);

    Task AddNonAuctionWages(List<OrderWithProductDto> orderDtos,
                   CancellationToken cancellationToken);

    Task FinalizeAuctionOrder(int customerId, int productId, 
                                decimal price, CancellationToken cancellationToken);

    Task<AuctionOrderDto> GetBestAuctionOrder(int productId,
                                           CancellationToken cancellationToken);

     Task DeactiveAuction(int productId, CancellationToken cancellationToken);

    Task AddAuctionWage(decimal price, int wage,
                        int boothId, int productId, CancellationToken cancellationToken);
}
