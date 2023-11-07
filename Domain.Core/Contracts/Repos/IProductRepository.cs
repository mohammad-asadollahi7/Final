using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Repos;

public interface IProductRepository
{
    Task<ProductDetailsDto?> GetNonAuctionProductById(int productId,
                                                      CancellationToken cancellationToken);

    Task<ProductDetailsDto?> GetAuctionProductById(int productId,
                                                   CancellationToken cancellationToken);

    Task<List<ProductInventoryDto>> GetProductInventories(int productId,
                                                          CancellationToken cancellationToken);

    Task<List<ProductOutputApprove>> GetProductsForApprove(CancellationToken cancellationToken);
    
    Task<SellType> GetSellType(int productId, CancellationToken cancellationToken);

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

    Task AddQuantityRecord(int productId,
                           int quantity,
                           DateTime submitDate,
                           bool isSold,
                           CancellationToken cancellationToken,
                           bool isCommit);

    Task AddPicture(int productId,
                    PictureDto pictureDto,
                    CancellationToken canellationToken,
                    bool isCommit);

    Task Update(int productId,
                string persianTitle,
                string englishTitle,
                string description,
                List<CustomAttributeDto> customAttributes,
                bool isCommit,
                CancellationToken cancellationToken);



    Task UpdateAuctionRecord(int productId,
                              DateTime fromDate,
                              DateTime toDate,
                              decimal minPrice,
                              bool isCommit,
                              CancellationToken cancellationToken);



     Task UpdateNonAuctionPrice(int productId,
                                decimal price,
                                int discount,
                                bool isCommit,
                                CancellationToken cancellationToken);

    Task<bool> Delete(int productId,
                      CancellationToken cancellationToken);

    Task<bool> IsDeleted(int productId,
                         CancellationToken cancellationToken);


    Task<bool> IsExistById(int id,
                           SellType sellType,
                           CancellationToken cancellationToken);

    Task<bool> IsExistById(int id, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
    
}
