using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataAccess.Repos.EF;

public class ProductRepository : IProductRepository
{
    private readonly FinalContext _context;

    public ProductRepository(FinalContext context) => _context = context;

    public async Task<ProductDetailsDto?> GetNonAuctionsById(int productId,
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
}
