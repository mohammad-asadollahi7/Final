﻿using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;

namespace Domain.AppServices;

public class BoothAppService : IBoothAppService
{
    private readonly IBoothService _boothService;

    public BoothAppService(IBoothService boothService)
    {
        _boothService = boothService;
    }
    public async Task<BoothDto> GetBoothBySellerId(int sellerId,
                                       CancellationToken cancellationToken)
    {
        return await _boothService.GetBoothBySellerId(sellerId, cancellationToken);
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title,
                                                       CancellationToken cancellationToken)
    {
        await _boothService.EnsureExistByTitle(title, cancellationToken);
        return await _boothService.GetNonAuctionsByBoothTitle(title, cancellationToken);
    }


    public async Task Create(int sellerId, string title, string description,
                            Picture picture, CancellationToken cancellationToken)
    {
        await _boothService.Create(sellerId, title, 
                                    description, picture, cancellationToken);   
    }


    public async Task Update(int boothId, UpdateBoothDto boothDto,
                                CancellationToken cancellationToken)
    {
        await _boothService.EnsureExistById(boothId, cancellationToken);
        await _boothService.Update(boothId, boothDto, cancellationToken);
    }

    public async Task Delete(int boothId, CancellationToken cancellationToken)
    {
        await _boothService.EnsureExistById(boothId, cancellationToken);
        await _boothService.Delete(boothId, cancellationToken);
    }

}