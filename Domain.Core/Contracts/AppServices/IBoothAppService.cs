﻿
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;

namespace Domain.Core.Contracts.AppServices;

public interface IBoothAppService
{
    Task<BoothDto> GetBoothBySellerId(int sellerId, CancellationToken cancellationToken);

    Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title,
                                            CancellationToken cancellationToken);

    Task Create(int sellerId,
                string title,
                string description,
                Picture picture,
                CancellationToken cancellationToken);

    Task Update(int boothId, UpdateBoothDto boothDto,
                    CancellationToken cancellationToken);

    Task Delete(int boothId, CancellationToken cancellationToken);
}