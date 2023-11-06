using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.Repos;

public interface IBoothRepository
{
    Task<BoothDto> GetBoothBySellerId(int sellerId);

    Task<ProductDetailsDto> GetProductsByBoothTitle(string title);

    Task Create(int sellerId,
                string title,
                string description,
                int wage,
                Medal medal,
                Picture picture);

    Task Update(int boothId, UpdateBoothDto boothDto);

    Task Delete(int boothId);
}