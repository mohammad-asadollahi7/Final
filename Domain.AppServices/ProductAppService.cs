using Domain.Core.Enums;
using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;

namespace Domain.AppServices;

public class ProductAppService : IProductAppService
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductAppService(IProductService productService,
                             ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(int categoryId,
                                                                         CancellationToken cancellationToken)
    {
        await _categoryService.EnsureExistById(categoryId, cancellationToken);
        var categoriesIds = await _categoryService.GetSubcategoriesIdsByCategoryId(categoryId, cancellationToken);
        var products = await _productService.GetNonAuctionsByCategoryId(cancellationToken, categoriesIds.ToArray());
        return products;
    }

    public async Task<ProductDetailsDto> GetAuctionProductById(int productId, CancellationToken cancellationToken)
    {
        await _productService.EnsureExistById(productId, SellType.Auction, cancellationToken);
        var productDto = await _productService.GetAuctionProductById(productId, cancellationToken);
        return productDto;
    }

    public async Task<ProductDetailsDto?> GetNonAuctionProductById(int productId, CancellationToken cancellationToken)
    {
        await _productService.EnsureExistById(productId, SellType.NonAuction, cancellationToken);
        var productDto = await _productService.GetNonAuctionProductById(productId, cancellationToken);
        return productDto;
    }

    public async Task CreateNonAuction(CreateNonAuctionProductDto createProduct,
                                       CancellationToken cancellationToken)
    {
        await _categoryService.EnsureCategoryIsLeaf(createProduct.CategoryId,
                                                    cancellationToken);

        var newProductId = await _productService.Create(createProduct.PersianTitle,
                                                        createProduct.EnglishTitle,
                                                        createProduct.Description,
                                                        createProduct.CategoryId,
                                                        createProduct.BoothId,
                                                        SellType.NonAuction,
                                                        false,
                                                        cancellationToken);

        await _productService.AddCustomAttributes(newProductId,
                                                  createProduct.CustomAttributes,
                                                  false,
                                                  cancellationToken);

        await _productService.AddNonAuctionPrice(createProduct.FirstPrice,
                                                 createProduct.FirstDiscount,
                                                 newProductId,
                                                 false,
                                                 cancellationToken);

        await _productService.AddQuantityRecord(newProductId,
                                                createProduct.FirstQuantity,
                                                DateTime.Now,
                                                false,
                                                cancellationToken,
                                                false);

        await _productService.AddPicture(newProductId,
                                         createProduct.PictureDto,
                                         cancellationToken, false);

        await _productService.SaveChangesAsync(cancellationToken);
    }


    public async Task CreateAuction(CreateAuctionProductDto createProduct,
                                    CancellationToken cancellationToken)
    {
        await _categoryService.EnsureCategoryIsLeaf(createProduct.CategoryId,
                                                    cancellationToken);

        var newProductId = await _productService.Create(createProduct.PersianTitle,
                                                        createProduct.EnglishTitle,
                                                        createProduct.Description,
                                                        createProduct.CategoryId,
                                                        createProduct.BoothId,
                                                        SellType.Auction,
                                                        false,
                                                        cancellationToken);

        await _productService.AddCustomAttributes(newProductId,
                                                  createProduct.CustomAttributes,
                                                  false,
                                                  cancellationToken);

        await _productService.AddAuction(newProductId,
                                         createProduct.FromDate,
                                         createProduct.ToDate,
                                         createProduct.MinPrice,
                                         false,
                                         cancellationToken);

        await _productService.AddQuantityRecord(newProductId,
                                                createProduct.FirstQuantity,
                                                DateTime.Now,
                                                false,
                                                cancellationToken,
                                                false);

        await _productService.AddPicture(newProductId,
                                         createProduct.PictureDto,
                                         cancellationToken, false);

        await _productService.SaveChangesAsync(cancellationToken);
    }


    public async Task Remove(int productId, CancellationToken cancellationToken)
    {
        await _productService.EnsureExistById(productId, null, cancellationToken);
        await _productService.Remove(productId, cancellationToken);
    }

    public async Task UpdateNonAuction(int productId, UpdateNonAuctionProductDto productDto,
                                       CancellationToken cancellationToken)
    {
        await _productService.UpdateNonAuctionProduct(productId,productDto,
                                                    false, cancellationToken);

        await _productService.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAuction(int productId, UpdateAuctionProductDto productDto,
                                       CancellationToken cancellationToken)
    {
        await _productService.UpdateAuctionProduct(productId, productDto,
                                                   false, cancellationToken);

        await _productService.SaveChangesAsync(cancellationToken);
    }
}
