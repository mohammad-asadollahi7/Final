using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace Infra.DataAccess.Repos.EF;

public class BoothRepository : IBoothRepository
{
    private readonly FinalContext _context;
    public BoothRepository(FinalContext context) => _context = context;
    

    public async Task<BoothDto?> GetBoothBySellerId(int sellerId,
                                                    CancellationToken cancellationToken)
    {
        return await _context.Booths.Where(b => b.SellerId == sellerId
                                                 && b.IsDeleted == false)
                                                .Select(b => new BoothDto()
                                                {
                                                    Id = b.Id,
                                                    Description = b.Description,
                                                    Medal = b.Medal,
                                                    Picture = b.Picture,
                                                    Title = b.Title,
                                                    Wage = b.Wage
                                                }).AsNoTracking()
                                                .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<List<ProductOutputDto>> GetProductsByBoothTitle(string title, 
                                                            CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => p.Booth.Title == title)
                                         .Select(p => new ProductOutputDto()
                                         {
                                             Id = p.Id,
                                             PersianTitle = p.PersianTitle,
                                             DiscountPercent = p.NonAuctionPrice.Discount,
                                             Price = p.NonAuctionPrice.Price,
                                             BoothTitle = p.Booth.Title,
                                             MainPicturePath = p.ProductPictures
                                                               .Select(p => p.Picture.Name).First(),
                                         }).AsNoTracking()
                                         .ToListAsync(cancellationToken);
    }


    public async Task Create(int sellerId, string title,
                             string description, int wage,
                             Medal medal, Picture picture,
                             CancellationToken cancellationToken)
    {
        var booth = new Booth()
        {
            Title = title,
            Description = description,
            Wage = wage,
            Medal = medal,
            Picture = picture,
            SellerId = sellerId,
            IsDeleted = false,
        };

        await _context.Booths.AddAsync(booth);
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task Update(int boothId, UpdateBoothDto boothDto, 
                                CancellationToken cancellationToken)
    {
        var booth = await _context.Booths.SingleOrDefaultAsync(b => b.Id == boothId, 
                                                                cancellationToken);
        booth.Picture = boothDto.Picture;
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
}
