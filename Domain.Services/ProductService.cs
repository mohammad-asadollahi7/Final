using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;
using Domain.Core.Exceptions;

namespace Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDapperRepository _productDapperRepository;

    public ProductService(IProductRepository productRepository,
                          IProductDapperRepository productDapperRepository)
    {
        _productRepository = productRepository;
        _productDapperRepository = productDapperRepository;
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(CancellationToken cancellationToken,
                                                                         params int[] ids)
    {
        var products = await _productDapperRepository.GetNonAuctionsByCategoryId(cancellationToken, ids);
        if (products.Count() == 0)
            throw new AppException(ExpMessage.HaveNotProduct,
                                   ExpStatusCode.BadRequest);

        return products;
    }

    public async Task<ProductDetailsDto?> GetNonAuctionProductById(int productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetNonAuctionProductById(productId, cancellationToken);

        if (product == null)
            throw new AppException(string.Format(ExpMessage.NotChangedProduct, "پیدا"),
                                   ExpStatusCode.NotFound);
        return product;
    }


    public async Task<ProductDetailsDto?> GetAuctionProductById(int productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAuctionProductById(productId, cancellationToken);

        if (product == null)
            throw new AppException(string.Format(ExpMessage.NotChangedProduct, "پیدا"),
                                   ExpStatusCode.NotFound);
        return product;
    }

    public async Task<List<ProductOutputApprove>> GetProductsForApprove(
                                                        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsForApprove(cancellationToken);

        if (products.Count() == 0)
            throw new AppException(ExpMessage.HaveNotProduct,
                                   ExpStatusCode.BadRequest);

        return products;
    }



    public async Task<SellType> GetSellType(int productId, CancellationToken cancellationToken)
    {
        return await _productRepository.GetSellType(productId, cancellationToken);
    }

    public async Task<int> Create(string persianTitle,
                                  string englishTitle,
                                  string description,
                                  int categoryId,
                                  int boothId,
                                  SellType sellType,
                                  bool isCommit,
                                  CancellationToken cancellationToken)
    {
        return await _productRepository.Create(persianTitle,
                                               englishTitle,
                                               description,
                                               categoryId,
                                               boothId,
                                               sellType,
                                               isCommit,
                                               cancellationToken);
    }



    public async Task AddCustomAttributes(int productId,
                                          List<CustomAttributeDto> attributes,
                                          bool isCommit,
                                          CancellationToken cancellationToken)
    {
        await _productRepository.AddCustomAttributes(productId,
                                                     attributes,
                                                     isCommit,
                                                     cancellationToken);
    }


    public async Task AddNonAuctionPrice(decimal price,
                                         int discount,
                                         int productId,
                                         bool isCommit,
                                         CancellationToken cancellationToken)
    {
        await _productRepository.AddNonAuctionPrice(price,
                                                    discount,
                                                    productId,
                                                    isCommit,
                                                    cancellationToken);
    }

    public async Task AddAuction(int productId,
                                 DateTime fromDate,
                                 DateTime toDate,
                                 decimal minPrice,
                                 bool isCommit,
                                 CancellationToken cancellationToken)
    {
        await _productRepository.AddAuction(productId,
                                            fromDate,
                                            toDate,
                                            minPrice,
                                            isCommit,
                                            cancellationToken);
    }


    public async Task AddPicture(int productId,
                                 PictureDto pictureDto,
                                 CancellationToken canellationToken,
                                 bool isCommit)
    {
        await _productRepository.AddPicture(productId,
                                            pictureDto,
                                            canellationToken,
                                            isCommit);
    }

    public async Task AddQuantityRecord(int productId,
                                        int quantity,
                                        DateTime submitDate,
                                        bool isSold,
                                        CancellationToken cancellationToken,
                                        bool isCommit)
    {
        if (quantity != 0)
            await _productRepository.AddQuantityRecord(productId,
                                                       quantity,
                                                       submitDate,
                                                       isSold,
                                                       cancellationToken,
                                                       isCommit);
    }


    public async Task UpdateAuctionProduct(int productId,
                                           UpdateAuctionProductDto productDto,
                                           bool isCommit,   
                                           CancellationToken cancellationToken)
    {
        await _productRepository.Update(productId,
                                        productDto.PersianTitle,
                                        productDto.EnglishTitle,
                                        productDto.Description,
                                        productDto.CustomAttributes,
                                        isCommit,
                                        cancellationToken);

        await _productRepository.UpdateAuctionRecord(productId, 
                                                     productDto.FromDate,
                                                     productDto.ToDate,
                                                     productDto.MinPrice,
                                                     isCommit, 
                                                     cancellationToken);
    }


    public async Task UpdateNonAuctionProduct(int productId,
                                            UpdateNonAuctionProductDto productDto,
                                            bool isCommit,
                                            CancellationToken cancellationToken)
    {

        await _productRepository.Update(productId,
                                        productDto.PersianTitle,
                                        productDto.EnglishTitle,
                                        productDto.Description,
                                        productDto.CustomAttributes,
                                        isCommit,
                                        cancellationToken);

        await _productRepository.UpdateNonAuctionPrice(productId,
                                                       productDto.Price,
                                                       productDto.Discount,
                                                       isCommit,
                                                       cancellationToken);
    }


    public async Task Remove(int productId,
                             CancellationToken cancellationToken)
    {
        var isRemoved = await _productRepository.Delete(productId, cancellationToken);
        if (!isRemoved)
            throw new AppException(string.Format(ExpMessage.NotChangedProduct, "حذف"),
                                   ExpStatusCode.InternalServerError);

    }


    public async Task EnsureProductQuantitySufficient(int productId,
                                                      int quantity,
                                                      CancellationToken cancellationToken)
    {
        var productInventories = await _productRepository.GetProductInventories(productId,
                                                                        cancellationToken);
        int currentQuantity = 0;
        foreach (var productInventory in productInventories)
        {
            if (productInventory.IsSold is true)
                productInventory.Quantity *= -1;

            currentQuantity += productInventory.Quantity;
        }

        if (currentQuantity < quantity)
            throw new AppException(ExpMessage.InsufficientProduct,
                                   ExpStatusCode.BadRequest);
    }



    public async Task EnsureExistById(int productId,
                                      SellType? sellType,
                                      CancellationToken cancellationToken)
    {
        bool isExist;
        if (sellType is not null)
            isExist = await _productRepository.IsExistById(productId, sellType ?? 0, cancellationToken);
        else
            isExist = await _productRepository.IsExistById(productId, cancellationToken);


        if (!isExist)
            throw new AppException(string.Format(ExpMessage.NotChangedProduct, "پیدا"),
                                   ExpStatusCode.NotFound);
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _productRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task ApproveProduct(int id, bool isApproved, 
                               CancellationToken cancellationToken)
    {
        await _productRepository.ApproveProduct(id, isApproved, cancellationToken);
    }
}
