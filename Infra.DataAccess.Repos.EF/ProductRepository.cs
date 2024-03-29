﻿using Domain.Core.Contracts.AppServices;
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

    public async Task<ProductDetailsDto?> GetNonAuctionProductById(int productId, bool? isApproved,
                                                                   CancellationToken cancellationToken)
    {
        var productDetailsDto = await _context.Products.Where(p => p.Id == productId && p.SellType == SellType.NonAuction 
                                                              && p.IsDeleted == false && p.IsApproved == isApproved)
                                                   .Select(p => new ProductDetailsDto()
                                                   {
                                                       Id = p.Id,
                                                       PersianTitle = p.PersianTitle,
                                                       EnglishTitle = p.EnglishTitle,
                                                       Description = p.Description,
                                                       SellType = p.SellType,
                                                       BoothTitle = p.Booth.Title,
                                                       CategoryId = p.Categories.First().Id,
                                                       Price = p.NonAuctionPrice.Price,
                                                       DiscountPercent = p.NonAuctionPrice.Discount,
                                                       BoothId = p.BoothId,
                                                       PictureName = p.ProductPictures.Select(p => p.Picture.Name).First(),
                                                       CustomAttributes = p.CustomAttributes.Select(c => new CustomAttributeDto()
                                                       {
                                                           Id = c.Id,
                                                           Title = c.AttributeTitle,
                                                           Value = c.AttributeValue
                                                       })

                                                   }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        return productDetailsDto;
    }


    public async Task<AuctionDetailsDto?> GetAuctionProductById(int productId, bool? isApproved,
                                                                CancellationToken cancellationToken)
    {
        var productDetailsDto = await _context.Products.Where(p => p.Id == productId && p.SellType == SellType.Auction
                                                              && p.IsDeleted == false && p.IsApproved == isApproved)
                                                  .Select(p => new AuctionDetailsDto()
                                                  {
                                                      Id = p.Id,
                                                      PersianTitle = p.PersianTitle,
                                                      EnglishTitle = p.EnglishTitle,
                                                      SellType = p.SellType,
                                                      FromDate = p.Auction.FromDate,
                                                      ToDate = p.Auction.ToDate,
                                                      CategoryId = p.Categories.First().Id,
                                                      MaxPrice = p.AuctionOrders.Select(a => a.Price).Max(),
                                                      Description = p.Description,
                                                      MinPrice = p.Auction.MinPrice,
                                                      BoothTitle = p.Booth.Title,
                                                      PictureName = p.ProductPictures.Select(p => p.Picture)
                                                                                           .Select(p => p.ProductPicture.Picture.Name).First(),
                                                      CustomAttributes = p.CustomAttributes.Select(c => new CustomAttributeDto()
                                                      {
                                                          Id = c.Id,
                                                          Title = c.AttributeTitle,
                                                          Value = c.AttributeValue
                                                      })

                                                  }).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        return productDetailsDto;
    }

    public async Task<List<ProductOutputApprove>> GetProductsForApprove(CancellationToken 
                                                                          cancellationToken)
    {
        return await _context.Products.Where(p => p.IsApproved == null && p.IsDeleted == false)
                               .Select(p => new ProductOutputApprove()
                               { 
                                   Id = p.Id,
                                   PersianTitle = p.PersianTitle,
                                   SellType = p.SellType,
                                   BoothTitle = p.Booth.Title,
                                   PicturesPath = p.ProductPictures.Select(pp => pp.Picture.Name)
                               }).ToListAsync(cancellationToken);
    }


    public async Task<SellType> GetSellType(int productId, CancellationToken cancellationToken)
    {
        return  (SellType)(await _context.Products.Where(p => p.Id == productId)
                                                  .Select(p => p.SellType)
                                                  .FirstOrDefaultAsync(cancellationToken));
    }

    public async Task<List<ProductInventoryDto>> GetProductInventories(int productId,
                                                                     CancellationToken cancellationToken)
    {

        return await _context.ProductInventories.Where(pi => pi.ProductId == productId)
                                                .Select(pi => new ProductInventoryDto()
                                                {
                                                    Id = pi.Id,
                                                    IsSold = pi.IsSold,
                                                    Quantity = pi.Quantity,
                                                    SellPrice = pi.SellPrice,
                                                }).ToListAsync(cancellationToken);
    }
    public async Task<int> Create(int sellerId,
                                  string persianTitle,
                                  string englishTitle,
                                  string description,
                                  int categoryId,
                                  SellType sellType,
                                  bool isCommit,
                                  CancellationToken cancellationToken)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
        var boothId = await _context.Booths.Where(b => b.SellerId == sellerId).Select(b => b.Id)
                                            .FirstOrDefaultAsync(cancellationToken);
        var newProduct = new Product()
        {
            PersianTitle = persianTitle,
            EnglishTitle = englishTitle,
            Description = description,
            IsDeleted = false,
            IsApproved = null,
            BoothId = boothId,
            SellType = sellType,
            
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
            MinPrice = minPrice,
            IsActive = true
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
            Quantity = quantity,
        };
        await _context.ProductInventories.AddAsync(productInventory);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task AddQuantityRecord(int cartId,
                                        DateTime submitDate,
                                        bool isSold,
                                        CancellationToken cancellationToken,
                                        bool isCommit)
    {
        var orders = await _context.Orders.Where(o => o.CartId == cartId)
                                    .Include(o => o.Product.NonAuctionPrice)
                                    .ToListAsync(cancellationToken);

        List<ProductInventory> productInventories = new();
        foreach(var order in orders)
        {
            var productInventory = new ProductInventory()
            { 
                ChangedAt = submitDate,
                IsSold = isSold,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                SellPrice = order.Product.NonAuctionPrice.Price * (1 - (order.Product.NonAuctionPrice.Discount)/100m)
            };
            productInventories.Add(productInventory);

        }

        await _context.ProductInventories.AddRangeAsync(productInventories, cancellationToken);
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
                             string persianTitle,
                             string englishTitle,
                             string description,
                             List<CustomAttributeDto> customAttributes,
                             bool isCommit,
                             CancellationToken cancellationToken)
    {
        var product = await _context.Products.Where(p => p.Id == productId)
                                             .Include(p => p.CustomAttributes)
                                             .FirstOrDefaultAsync(cancellationToken);

        product.PersianTitle = persianTitle;
        product.EnglishTitle = englishTitle;
        product.Description = description;

        List<CustomAttributes> newCustomAttributes = new();
        var attributes = product.CustomAttributes.ToList();
        foreach (var attribute in attributes)
        {
            var customAttribute = new CustomAttributes()
            {
                Id = attribute.Id,
                ProductId = product.Id,
                AttributeTitle = attribute.AttributeTitle,
                AttributeValue = customAttributes.Where(c => c.Id == attribute.Id
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
                                          DateTime fromDate,
                                          DateTime toDate,
                                          decimal minPrice,
                                          bool isCommit,
                                          CancellationToken cancellationToken)
    {
        var auction = await _context.Auctions.
                            FirstOrDefaultAsync(a => a.ProductId == productId,
                                                cancellationToken);
        auction.FromDate = fromDate;
        auction.ToDate = toDate;
        auction.MinPrice = minPrice;


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
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId, 
                                                                    cancellationToken);
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



    public async Task<bool> IsExistById(int id, bool? isApproved,
                                        SellType sellType,
                                        CancellationToken cancellationToken)
    {
        return await _context.Products.AnyAsync(p => p.Id == id &&
                                        p.SellType == sellType &&
                                        p.IsApproved == isApproved &&
                                        p.IsDeleted == false, 
                                        cancellationToken);
    }

    public async Task<bool> IsExistById(int id, bool? isApproved,
                                        CancellationToken cancellationToken)
    {
        return await _context.Products.AnyAsync(p => p.Id == id && p.IsDeleted == false
                                            && p.IsApproved == isApproved,
                                            cancellationToken);
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ApproveProduct(int id, bool isApproved,
                                           CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, 
                                                                    cancellationToken);
        product.IsApproved = isApproved;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<WageDto>> GetWages(CancellationToken cancellationToken)
    {
        return await _context.Wages.Select(w => new WageDto()
                                 {
                                     Id = w.Id,
                                     BoothTitle = w.Booth.Title,
                                     Date = w.Date,
                                     ProductTitle = w.Product.PersianTitle,
                                     Wages = w.Wages
                                 }).ToListAsync(cancellationToken);
       
    }

    public async Task<int> GetNumberOfProductsForApprove(CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => p.IsApproved == null).CountAsync(cancellationToken);
    }

    public async Task<int> GetWageNumbers(CancellationToken cancellationToken)
    {
        return await _context.Wages.CountAsync(cancellationToken);
    }

    public async Task<List<ProductOutputDto>> GetAuctions(bool? isApproved,
                                            CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => p.IsDeleted == false
                                && p.IsApproved == isApproved
                                && p.SellType == SellType.Auction
                                && p.Auction.ToDate > DateTime.Now
                                & p.Auction.IsActive == true)
                                .Select(p => new ProductOutputDto()
                                {
                                    Id = p.Id,
                                    BoothTitle = p.Booth.Title,
                                    PicturesName = p.ProductPictures.Select(p => p.Picture.Name).First(),
                                    PersianTitle = p.PersianTitle,
                                    Price = p.AuctionOrders.Select(p => p.Price).Max(),
                                    SellType = SellType.Auction,
                                }).ToListAsync(cancellationToken);
    }

    public async Task AddWages(List<CreateWageDto> createWageDtos,
                               CancellationToken cancellationToken)
    {
        List<Wage> wages = new();

        foreach (var createWageDto in createWageDtos)
        {
            var newWage = new Wage()
            {
                BoothId = createWageDto.BoothId,
                Date = createWageDto.Date,
                ProductId = createWageDto.ProductId,
                Wages = createWageDto.FinalWage
            };
            wages.Add(newWage);
        }

        await _context.Wages.AddRangeAsync(wages, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task FinalizeAuctionOrder(int customerId, int productId, 
                                decimal price, CancellationToken cancellationToken)
    {
        var newOrder = new FinalAuctionOrder()
        {
            CustomerId = customerId,
            Price = price,
            ProductId = productId
        };

        await _context.FinalAuctionOrders.AddAsync(newOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuctionOrderDto> GetBestAuctionOrder(int productId,
                                             CancellationToken cancellationToken)
    {
        return await _context.AuctionOrders.Where(a => a.ProductId == productId)
                                .OrderByDescending(a => a.Price).Take(1).Select(a => new AuctionOrderDto()
                                {
                                    CustomerId = a.CustomerId,
                                    MaxPrice = a.Price,
                                }).FirstAsync(cancellationToken);
    }

    public async Task DeactiveAuction(int productId, CancellationToken cancellationToken)
    {
        var order = await _context.Auctions.FirstOrDefaultAsync(a => a.ProductId == productId);
        order.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAuctionWage(decimal finalWage, DateTime date,
                               int boothId, int productId, CancellationToken cancellationToken)
    {
        var newWage = new Wage()
        {
            BoothId = boothId,
            Date = date,
            ProductId = productId,
            Wages = finalWage
        };
        await _context.Wages.AddAsync(newWage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}