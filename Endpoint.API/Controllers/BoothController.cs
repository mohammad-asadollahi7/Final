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


    [HttpGet("GetBySellerId")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> GetBoothBySellerId(CancellationToken cancellationToken)
    {
        var booth = await _boothAppService.GetBySellerId(CurrentUserId, 
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
    public async Task<IActionResult> Create(CreateBoothDto boothDto,
                                             CancellationToken cancellationToken)
    {
        await _boothAppService.Create(boothDto, CurrentUserId, cancellationToken);
        return Ok();
    }


    [HttpPut("Update/boothId")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> Update(int boothId, UpdateBoothDto boothDto,
                                              CancellationToken cancellationToken)
    {
        await _boothAppService.Update(boothId, boothDto, cancellationToken);
        return Ok();
    }

    [HttpDelete("Delete/boothId")]
    //[HaveAccess(Role.Seller, Role.Admin)]
    public async Task<IActionResult> Delete(int boothId, 
                                        CancellationToken cancellationToken)
    {
        await _boothAppService.Delete(boothId, cancellationToken);
        return Ok();    
    }

    [HttpGet("GetById/{boothId}")]
    //[HaveAccess(Role.Admin, Role.Customer)]
    public async Task<IActionResult> GetById(int boothId, 
                                             CancellationToken cancellationToken)
    {
        var booth = await _boothAppService.GetById(boothId, cancellationToken);
        return Ok(booth);
    }


    [HttpGet("GetByTitle/{title}")]
    //[HaveAccess(Role.Admin, Role.Customer)]
    public async Task<IActionResult> GetByTitle(string title,
                                              CancellationToken cancellationToken)
    {
        var booth = await _boothAppService.GetByTitle(title, cancellationToken);
        return Ok(booth);
    }


    [HttpGet("GetNonAuctionsBySellerId")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> GetNonAuctionsBySellerId(CancellationToken cancellationToken)
    {
        var products = await _boothAppService.GetNonAuctionsBySellerId(CurrentUserId, 
                                                                       cancellationToken);
        return Ok(products);
    }



    [HttpGet("GetAuctionsBySellerId")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> GetAuctionsBySellerId(CancellationToken cancellationToken)
    {
        var products = await _boothAppService.GetAuctionsBySellerId(CurrentUserId,
                                                                    cancellationToken);
        return Ok(products);
    }

}
