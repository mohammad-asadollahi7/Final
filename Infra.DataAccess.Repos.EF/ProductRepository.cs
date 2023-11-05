using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;

public class ProductRepository : IProductRepository
{
    private readonly FinalContext _context;

    public ProductRepository(FinalContext context) => _context = context;

    public async Task<ProductDetailsDto?> GetNonAuctionProductById(int productId,
                                                            CancellationToken cancellationToken)
    {
        var productDetailsDto = await _context.Products.Where(p => p.Id == productId)
                                                   .Select(p => new ProductDetailsDto()
                                                   {
                                                       Id = p.Id,
                                                       PersianTitle = p.PersianTitle,
                                                       EnglishTitle = p.EnglishTitle,
                                                       Description = p.Description,
                                                       Price = p.NonAuctionPrice.Price,
                                                       DiscountPercent = p.NonAuctionPrice.Discount,
                                                       BoothId = p.BoothId,
                                                       ProductPictureDto = p.ProductPictures.Select(p => p.Picture)
                                                                                            .Select(p => new ProductPictureDto()
                                                                                            {
                                                                                                Id = p.Id,
                                                                                                Name = p.Name,
                                                                                            }),
                                                       CustomAttributes = p.CustomAttributes.Select(c => new CustomAttributeDto()
                                                       {
                                                           Id = c.Id,
                                                           Title = c.AttributeTitle,
                                                           Value = c.AttributeValue
                                                       })

                                                   }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        return productDetailsDto;
    }


    public async Task<ProductDetailsDto?> GetAuctionProductById(int productId,
                                                                CancellationToken cancellationToken)
    {
        var productDetailsDto = await _context.Products.Where(p => p.Id == productId)
                                                  .Select(p => new ProductDetailsDto()
                                                  {
                                                      Id = p.Id,
                                                      PersianTitle = p.PersianTitle,
                                                      EnglishTitle = p.EnglishTitle,
                                                      Description = p.Description,
                                                      Price = p.AuctionOrders.Select(a => a.Price).Max(),
                                                      BoothId = p.BoothId,
                                                      ProductPictureDto = p.ProductPictures.Select(p => p.Picture)
                                                                                           .Select(p => new ProductPictureDto()
                                                                                           {
                                                                                               Id = p.Id,
                                                                                               Name = p.Name,
                                                                                           }),
                                                      CustomAttributes = p.CustomAttributes.Select(c => new CustomAttributeDto()
                                                      {
                                                          Id = c.Id,
                                                          Title = c.AttributeTitle,
                                                          Value = c.AttributeValue
                                                      })

                                                  }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        return productDetailsDto;
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

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        var newProduct = new Product()
        {
            PersianTitle = persianTitle,
            EnglishTitle = englishTitle,
            Description = description,
            IsDeleted = false,
            IsApproved = false,
            BoothId = boothId,
            SellType = (int)sellType,
        };
        newProduct.Categories.Add(category);


        await _context.Products.AddAsync(newProduct, cancellationToken);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);

        return _context.Entry(newProduct).Property(p => p.Id).CurrentValue;
    }


    public async Task AddCustomAttributes(int productId,
                                          List<CustomAttributeDto> attributes,
                                          bool isCommit,
                                          CancellationToken cancellationToken)
    {
        List<CustomAttributes> customAttributes = new();
        foreach (var attribute in attributes)
        {
            var customAttribute = new CustomAttributes()
            {
                ProductId = productId,
                AttributeTitle = attribute.Title,
                AttributeValue = attribute.Value,
            };
            customAttributes.Add(customAttribute);
        }

        await _context.CustomAttributes.AddRangeAsync(customAttributes);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task AddNonAuctionPrice(decimal price,
                                         int discount,
                                         int productId,
                                         bool isCommit,
                                         CancellationToken cancellationToken)
    {
        var nonAuctionPrice = new NonAuctionPrice()
        {
            Price = price,
            Discount = discount,
            ProductId = productId
        };

        await _context.NonAuctionPrices.AddAsync(nonAuctionPrice, cancellationToken);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAuction(int productId,
                                 DateTime fromDate,
                                 DateTime toDate,
                                 decimal minPrice,
                                 bool isCommit,
                                 CancellationToken cancellationToken)
    {
        var auction = new Auction()
        {
            ProductId = productId,
            FromDate = fromDate,
            ToDate = toDate,
            MinPrice = minPrice
        };

        await _context.Auctions.AddAsync(auction, cancellationToken);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task AddQuantityRecord(int productId,
                                        int quantity,
                                        DateTime submitDate,
                                        bool isSold,
                                        CancellationToken cancellationToken,
                                        bool isCommit)
    {
        var productInventory = new ProductInventory()
        {
            IsSold = isSold,
            ChangedAt = submitDate,
            ProductId = productId,
            Quantity = quantity
        };
        await _context.ProductInventories.AddAsync(productInventory);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPicture(int productId,
                                 PictureDto pictureDto,
                                 CancellationToken canellationToken,
                                 bool isCommit)
    {
        List<Picture> pictures = new();
        List<ProductPicture> productPictures = new();

        var newPicture = new Picture()
        {
            Name = pictureDto.PictureName,
        };
        var newProductPicture = new ProductPicture()
        {
            ProductId = productId,
            Picture = newPicture
        };
        pictures.Add(newPicture);
        productPictures.Add(newProductPicture);


        await _context.Pictures.AddRangeAsync(pictures, canellationToken);
        await _context.ProductPictures.AddRangeAsync(productPictures, canellationToken);


        if (isCommit)
            await _context.SaveChangesAsync(canellationToken);
    }


    public async Task Update(int productId,
                             UpdateProductDto productDto,
                             bool isCommit,
                             CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(p => p.Id == productId)
                                             .Include(p => p.CustomAttributes)
                                             .FirstOrDefaultAsync(cancellationToken);

        product.PersianTitle = productDto.PersianTitle;
        product.EnglishTitle = productDto.EnglishTitle;
        product.Description = productDto.Description;

        List<CustomAttributes> newCustomAttributes = new();
        var attributes = product.CustomAttributes.ToList();
        foreach (var attribute in attributes)
        {
            var customAttribute = new CustomAttributes()
            {
                Id = attribute.Id,
                ProductId = product.Id,
                AttributeTitle = attribute.AttributeTitle,
                AttributeValue = productDto.CustomAttributes.Where(c => c.Id == attribute.Id
                                                        && c.Title == attribute.AttributeTitle)
                                                        .Select(c => c.Value).First()
            };
            newCustomAttributes.Add(customAttribute);
        }
        product.CustomAttributes = newCustomAttributes;

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAuctionRecord(int productId,
                                          AuctionDto auctionDto,
                                          bool isCommit,
                                          CancellationToken cancellationToken)
    {
        var auction = await _context.Auctions.FirstOrDefaultAsync(a => a.ProductId == productId,
                                                                  cancellationToken);
        auction.FromDate = auctionDto.FromDate;
        auction.ToDate = auctionDto.ToDate;
        auction.MinPrice = auctionDto.MinPrice;


        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }  

    public async Task UpdateNonAuctionPrice(int productId,
                                            decimal price,
                                            int discount,
                                            bool isCommit,
                                            CancellationToken cancellationToken)
    {
        var nonAuctionPrice = await _context.NonAuctionPrices.FirstOrDefaultAsync
                                                              (n => n.ProductId == productId);
        nonAuctionPrice.Price = price;
        nonAuctionPrice.Discount = discount;

        if(isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<bool> Delete(int productId,
                                   CancellationToken cancellationToken)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId, cancellationToken);
        product.IsDeleted = true;
        var isAffected = await _context.SaveChangesAsync(cancellationToken);
        return isAffected == 1;
    }

    public async Task<bool> IsDeleted(int productId,
                                      CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId,
                                                                  cancellationToken);
        return product.IsDeleted;
    }



    public async Task<bool> IsExistById(int id,
                                        CancellationToken cancellationToken)
    {
        return await _context.Products.AnyAsync(p => p.Id == id, cancellationToken);
    }



    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

}

