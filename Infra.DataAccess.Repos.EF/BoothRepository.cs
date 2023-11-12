using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Pictures;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;


namespace Infra.DataAccess.Repos.EF;

public class BoothRepository : IBoothRepository
{
    private readonly FinalContext _context;
    public BoothRepository(FinalContext context) => _context = context;


    public async Task<BoothDto?> GetBySellerId(int sellerId,
                                                    CancellationToken cancellationToken)
    {
        return await _context.Booths.Where(b => b.SellerId == sellerId
                                                 && b.IsDeleted == false)
                                                .Select(b => new BoothDto()
                                                {
                                                    Id = b.Id,
                                                    Description = b.Description,
                                                    Medal = b.Medal,
                                                    PictureName = b.BoothPicture.Picture.Name,
                                                    Title = b.Title,
                                                    Wage = b.Wage
                                                }).AsNoTracking()
                                                .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title,
                                                            CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => p.Booth.Title == title 
                                        && p.SellType == SellType.NonAuction
                                        && p.IsDeleted == false)
                                         .Select(p => new ProductOutputDto()
                                         {
                                             Id = p.Id,
                                             PersianTitle = p.PersianTitle,
                                             DiscountPercent = p.NonAuctionPrice.Discount,
                                             Price = p.NonAuctionPrice.Price,
                                             BoothTitle = p.Booth.Title,
                                             SellType = p.SellType,
                                             MainPicturePath = p.ProductPictures
                                                               .Select(p => p.Picture.Name).First(),
                                         }).AsNoTracking()
                                         .ToListAsync(cancellationToken);
    }



    public async Task<List<ProductInventoryDto>> GetInventoriesByBoothId(int boothId,
                                                               CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => p.BoothId == boothId)
                    .SelectMany(p => p.ProductInventories
                    .Select(pi => new ProductInventoryDto()
                    {
                        Id = pi.Id,
                        IsSold = pi.IsSold,
                        Quantity = pi.Quantity,
                        SellPrice = pi.SellPrice
                    })).ToListAsync(cancellationToken);
    }


    public async Task Create(CreateBoothDto boothDto,
                             int sellerId,
                             int wage,
                             Medal medal,
                             bool isDeleted,
                             CancellationToken cancellationToken)
    {
        var newBooth = new Booth()
        {
            Title = boothDto.Title,
            Description = boothDto.Description,
            Wage = wage,
            Medal = medal,
            SellerId = sellerId,
            IsDeleted = isDeleted,
        };

        var newPicture = new Picture()
        {
            Name = boothDto.PictureName
        };
        newBooth.BoothPicture = new BoothPicture()
        {
            Picture = newPicture
        };

        await _context.Booths.AddAsync(newBooth);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task Update(int boothId, UpdateBoothDto boothDto,
                                CancellationToken cancellationToken)
    {
        var booth = await _context.Booths.Where(b => b.Id == boothId)
                            .Include(b => b.BoothPicture)
                            .ThenInclude(b => b.Picture)
                            .FirstOrDefaultAsync(cancellationToken);

        booth.BoothPicture.Picture.Name = boothDto.PictureName; 
        booth.Description = boothDto.Description;
        booth.Title = boothDto.Title;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWage(int boothId, int wage,
                                  Medal medal, CancellationToken cancellationToken)
    {
        await _context.Booths.SingleOrDefaultAsync(b => b.Id == boothId);
    }


    public async Task Delete(int boothId, CancellationToken cancellationToken)
    {
        var booth = await _context.Booths.SingleOrDefaultAsync(b => b.Id == boothId,
                                                               cancellationToken);
        booth.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }


    public Task<bool> IsExistById(int id, CancellationToken cancellationToken)
    {
        return _context.Booths.AnyAsync(b => b.Id == id && b.IsDeleted == false,
                                        cancellationToken);
    }

    public async Task<bool> IsExistByTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Booths.AnyAsync(b => b.Title.ToLower() == title.ToLower()
                                              && b.IsDeleted == false,
                                              cancellationToken);
    }

    public async Task<BoothDto?> GetById(int boothId, CancellationToken cancellationToken)
    {
         return await _context.Booths.Where(b => b.Id == boothId)
                                    .Select(b => new BoothDto()
         {
             Id = b.Id,
             Description = b.Description,   
             PictureName = b.BoothPicture.Picture.Name,
             Medal = b.Medal,
             Title = b.Title,
             Wage = b.Wage
         }).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<BoothDto?> GetByTitle(string title, 
                                    CancellationToken cancellationToken)
    {
        return await _context.Booths
                        .Where(b => b.Title.ToLower() == title.ToLower())
                        .Select(b => new BoothDto()
                        {
                            Id = b.Id,
                            Description = b.Description,
                            PictureName = b.BoothPicture.Picture.Name,
                            Medal = b.Medal,
                            Title = b.Title,
                            Wage = b.Wage
                        }).FirstOrDefaultAsync(cancellationToken);
    }
}
