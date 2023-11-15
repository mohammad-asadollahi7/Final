using Domain.Core.Enums;
using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using System.Globalization;

namespace Domain.AppServices;

public class ProductAppService : IProductAppService
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly ICartService _cartService;
    private readonly IBoothService _boothService;

    public ProductAppService(IProductService productService,
                             ICategoryService categoryService,
                             ICartService cartService,
                             IBoothService boothService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _cartService = cartService;
        _boothService = boothService;
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(int categoryId,
                                                                   CancellationToken cancellationToken)
    {
        await _categoryService.EnsureExistById(categoryId, cancellationToken);
        var categoriesIds = await _categoryService.GetSubcategoriesIdsByCategoryId(categoryId, 
                                                                                cancellationToken);
        var products = await _productService.GetNonAuctionsByCategoryId(cancellationToken,
                                                                       categoriesIds.ToArray());
        return products;
    }

    public async Task<AuctionDetailsDto> GetAuctionProductById(int productId, bool? isApproved,
                                                            CancellationToken cancellationToken)
    {
        await _productService.EnsureExistById(productId, isApproved, SellType.Auction, cancellationToken);
        var productDto = await _productService.GetAuctionProductById(productId, isApproved, cancellationToken);
        return productDto;
    }

    public async Task<ProductDetailsDto> GetNonAuctionProductById(int productId, bool? isApproved,
                                                        CancellationToken cancellationToken)
    {
        await _productService.EnsureExistById(productId, isApproved, SellType.NonAuction, cancellationToken);
        var productDto = await _productService.GetNonAuctionProductById(productId, isApproved, cancellationToken);
        return productDto;
    }

    public async Task<List<ProductOutputApprove>> GetProductsForApprove(
                                                        CancellationToken cancellationToken)
    {
        return await _productService.GetProductsForApprove(cancellationToken);
    }


    public async Task CreateNonAuction(int sellerId, CreateNonAuctionProductDto createProduct,
                                       CancellationToken cancellationToken)
    {
        await _categoryService.EnsureCategoryIsLeaf(createProduct.CategoryId,
                                                    cancellationToken);

        await _boothService.EnsureExistBySellerId(sellerId, cancellationToken);

         var newProductId = await _productService.Create(sellerId,
                                                        createProduct.PersianTitle,
                                                        createProduct.EnglishTitle,
                                                        createProduct.Description,
                                                        createProduct.CategoryId,
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


    public async Task CreateAuction(int sellerId,
                                    CreateAuctionProductDto createProduct,
                                    CancellationToken cancellationToken)
    {
        await _categoryService.EnsureCategoryIsLeaf(createProduct.CategoryId,
                                                    cancellationToken);

        await _boothService.EnsureExistBySellerId(sellerId, cancellationToken);

        var newProductId = await _productService.Create(sellerId,
                                                        createProduct.PersianTitle,
                                                        createProduct.EnglishTitle,
                                                        createProduct.Description,
                                                        createProduct.CategoryId,
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

        
        await _cartService.AddAuctionOrder(null, 
                                           newProductId,
                                           0,
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
        await _productService.EnsureExistById(productId, true, null, cancellationToken);
        await _productService.Remove(productId, cancellationToken);
    }

    public async Task UpdateNonAuction(int productId, 
                                       UpdateNonAuctionProductDto productDto,
                                       CancellationToken cancellationToken)
    {
        await _productService.UpdateNonAuctionProduct(productId,productDto,
                                                    false, cancellationToken);

        await _productService.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAuction(int productId, AuctionDetailsDto productDto,
                                       CancellationToken cancellationToken)
    {
        await _productService.UpdateAuctionProduct(productId, productDto,
                                                   false, cancellationToken);

        await _productService.SaveChangesAsync(cancellationToken);
    }

    public async Task ApproveProduct(int id, bool isApproved,
                                CancellationToken cancellationToken)
    {
        await _productService.EnsureExistById(id, null, null, cancellationToken);
        await _productService.ApproveProduct(id, isApproved, cancellationToken);
    }

    public async Task<List<WageDto>> GetWages(CancellationToken cancellationToken)
    {
       return await _productService.GetWages(cancellationToken);
    }

    public async Task<int> GetNumberOfProductsForApprove(CancellationToken cancellationToken)
    {
        return await _productService.GetNumberOfProductsForApprove(cancellationToken);
    }

    public async Task<int> GetWageNumbers(CancellationToken cancellationToken)
    {
        return await _productService.GetWageNumbers(cancellationToken);
    }

    public async Task<List<ProductOutputDto>> GetAuctions(bool? isApproved,
                                            CancellationToken cancellationToken)
    {
        return await _productService.GetAuctions(isApproved, cancellationToken);
    }

    public async Task<List<ProductOutputDto>> GetAuctionsBySellerId(int id, 
                                                            CancellationToken cancellationToken)
    {
        return await _boothService.GetAuctionsBySellerId(id, cancellationToken);
    }
}
