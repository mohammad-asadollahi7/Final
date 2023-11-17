
using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Cart;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace Domain.Services;

public class BoothService : IBoothService
{
    private readonly IBoothRepository _boothRepository;
    private readonly IBoothDapperRepository _boothDapperRepository;

    public BoothService(IBoothRepository boothRepository, IBoothDapperRepository boothDapperRepository)
    {
        _boothRepository = boothRepository;
        _boothDapperRepository = boothDapperRepository;
    }

    public async Task<BoothDto> GetBoothBySellerId(int sellerId,
                                       CancellationToken cancellationToken)
    {
        var booth = await _boothRepository.GetBySellerId(sellerId, cancellationToken);
        if (booth == null)
            throw new AppException(ExpMessage.NotFoundBooth,
                                    ExpStatusCode.NotFound);

        return booth;
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsByBoothTitle(string title,
                                                       CancellationToken cancellationToken)
    {
        var products = await _boothRepository.GetNonAuctionsByBoothTitle(title,
                                                                    cancellationToken);
        if (products.Count() == 0)
            throw new AppException(ExpMessage.HaveNotProduct,
                                   ExpStatusCode.NotFound);

        return products;
    }


    public async Task Create(CreateBoothDto boothDto, int sellerId,
                             CancellationToken cancellationToken)
    {
        await _boothRepository.Create(boothDto,
                                      sellerId,
                                      20,
                                      Medal.Bronze,
                                      false,
                                      cancellationToken);
    }


    public async Task Update(int boothId, UpdateBoothDto boothDto,
                                CancellationToken cancellationToken)
    {
        await _boothRepository.Update(boothId,
                                      boothDto,
                                      cancellationToken);
    }

    public async Task UpdateMedal(List<int> boothIds,
                                 CancellationToken cancellationToken)
    {

        var productInventories = await _boothDapperRepository.GetInventoriesByBoothIds(boothIds,
                                                                             cancellationToken);
        List<UpdateMedalDto> updateMedalDtos = new();
        for (int i = 0; i < boothIds.Count(); i++)
        {
            var newMedalDto = new UpdateMedalDto();
            newMedalDto.BoothId = boothIds[i];
            decimal TotalSales = 0;
            Medal medal = Medal.Bronze;
            int wage = 20;
            foreach (var productInventory in productInventories)
            {
                if (productInventory.BoothId == boothIds[i])
                {
                   
                    if (productInventory.IsSold == true && productInventory.SellPrice != null)
                    {
                        var price = productInventory.SellPrice ?? 0;
                        var quantity = productInventory.Quantity;
                        var totalPrice = price * quantity;
                        TotalSales += totalPrice;
                    }
                    if (TotalSales >= 10000000)
                    {
                        wage = 5;
                        medal = Medal.Gold;
                    }
                    else if (TotalSales > 5000000)
                    {
                        wage = 10;
                        medal = Medal.Silver;
                    }
                    else
                    {
                        wage = 20;
                        medal = Medal.Bronze;
                    }
                }
            }
            newMedalDto.Wage = wage;
            newMedalDto.Medal = medal;
            updateMedalDtos.Add(newMedalDto);
        }
        await _boothDapperRepository.UpdateMedal(updateMedalDtos, cancellationToken);

    }

    public async Task Delete(int boothId, CancellationToken cancellationToken)
    {
        await _boothRepository.Delete(boothId, cancellationToken);
    }



    public async Task EnsureExistById(int id, CancellationToken cancellationToken)
    {
        var isExist = await _boothRepository.IsExistById(id, cancellationToken);
        if (!isExist)
            throw new AppException(ExpMessage.NotFoundBooth,
                                    ExpStatusCode.NotFound);
    }

    public async Task EnsureExistByTitle(string title, CancellationToken cancellationToken)
    {
        var isExist = await _boothRepository.IsExistByTitle(title, cancellationToken);
        if (!isExist)
            throw new AppException(ExpMessage.NotFoundBooth,
                                    ExpStatusCode.NotFound);
    }

    public async Task<BoothDto> GetById(int boothId, CancellationToken cancellationToken)
    {
        var booth = await _boothRepository.GetById(boothId, cancellationToken);
        if (booth == null)
            throw new AppException(ExpMessage.NotFoundBooth,
                                    ExpStatusCode.NotFound);

        return booth;
    }

    public async Task<BoothDto> GetByTitle(string title,
                                            CancellationToken cancellationToken)
    {
        var booth = await _boothRepository.GetByTitle(title, cancellationToken);
        if (booth == null)
            throw new AppException(ExpMessage.NotFoundBooth,
                                    ExpStatusCode.NotFound);

        return booth;
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsBySellerId(int id,
                                            CancellationToken cancellationToken)
    {
        var products = await _boothRepository.GetNonAuctionsBySellerId(id, cancellationToken);
        if (products.Count() == 0)
            throw new AppException(ExpMessage.HaveNotProduct,
                                   ExpStatusCode.NotFound);

        return products;
    }

    public async Task EnsureExistBySellerId(int sellerId,
                                       CancellationToken cancellationToken)
    {
        var isExist = await _boothRepository.IsExistBySellerId(sellerId, cancellationToken);
        if (!isExist)
            throw new AppException(ExpMessage.NotFoundBooth,
                                   ExpStatusCode.NotFound);
    }

    public async Task EnsureNotExistBySellerId(int sellerId,
                                          CancellationToken cancellationToken)
    {
        var isExist = await _boothRepository.IsExistBySellerId(sellerId, cancellationToken);
        if (isExist)
            throw new AppException(ExpMessage.FoundBooth,
                                   ExpStatusCode.Conflict);
    }


    public async Task<List<ProductOutputDto>> GetAuctionsBySellerId(int id,
                                                        CancellationToken cancellationToken)
    {
        var products = await _boothRepository.GetAuctionsBySellerId(id, cancellationToken);
        if (products.Count() == 0)
            throw new AppException(ExpMessage.HaveNotProduct,
                                   ExpStatusCode.NotFound);

        return products;
    }

    public async Task DeleteProductsOfDeletedBooth(int boothId,
                                            CancellationToken cancellationToken)
    {
        await _boothRepository.DeleteProductsOfDeletedBooth(boothId, cancellationToken);
    }
}
