using Domain.Core.Contracts.AppServices;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Endpoint.API.CustomAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.API.Controllers;

public class BoothController : BaseController
{
    private readonly IBoothAppService _boothAppService;

    public BoothController(IBoothAppService boothAppService)
    {
        _boothAppService = boothAppService;
    }


    [HttpGet("GetBoothBySellerId")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> GetBoothBySellerId(CancellationToken cancellationToken)
    {
        var booth = await _boothAppService.GetBoothBySellerId(CurrentUserId, 
                                                         cancellationToken);
        return Ok(booth); 
    }


    [HttpGet("GetNonAuctionsByBoothTitle/{title}")]
    //[HaveAccess(Role.Customer)]
    public async Task<IActionResult> GetNonAuctionsByBoothTitle(string title,
                                            CancellationToken cancellationToken)
    {
        var products = await _boothAppService.GetNonAuctionsByBoothTitle(title,     
                                                                    cancellationToken);
        return Ok(products);
    }


    [HttpPost("Create")]
    //[HaveAccess(Role.Customer)]
    public async Task Create(CreateBoothDto boothDto,
                             CancellationToken cancellationToken)
    {
        await _boothAppService.Create(boothDto, CurrentUserId, cancellationToken);
    }


    [HttpPut("Update/boothId")]
    //[HaveAccess(Role.Seller)]
    public async Task Update(int boothId, UpdateBoothDto boothDto,
                               CancellationToken cancellationToken)
    {
        await _boothAppService.Update(boothId, boothDto, cancellationToken);
    }

    [HttpDelete("Delete/boothId")]
    //[HaveAccess(Role.Seller, Role.Admin)]
    public async Task Delete(int boothId, CancellationToken cancellationToken)
    {
        await _boothAppService.Delete(boothId, cancellationToken);
    }

}
