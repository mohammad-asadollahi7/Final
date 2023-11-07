﻿using Domain.Core.Contracts.AppServices;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;
using Endpoint.API.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.API.Controllers;
public class ProductController : BaseController
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }


    [HttpGet("GetNonAuctionsByCategoryId/{categoryId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNonAuctionsByCategoryId(int categoryId,
                                                                 CancellationToken cancellationToken)
    {
        var products = await _productAppService.GetNonAuctionsByCategoryId(categoryId, cancellationToken);
        return Ok(products);
    }


    [HttpGet("GetAuctionProductById/{productId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAuctionProductById(int productId,
                                                           CancellationToken cancellationToken)
    {
        var product = await _productAppService.GetAuctionProductById(productId, cancellationToken);
        return Ok(product);
    }


    [HttpGet("GetNonAuctionProductById/{productId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNonAuctionProductById(int productId,
                                                             CancellationToken cancellationToken)
    {
        var product = await _productAppService.GetNonAuctionProductById(productId, cancellationToken);
        return Ok(product);
    }

    [HttpPost("CreateNonAuction")]
    [HaveAccess(Role.Seller)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNonAuction(CreateNonAuctionProductDto createProduct,
                                       CancellationToken cancellationToken)
    {
        await _productAppService.CreateNonAuction(createProduct, cancellationToken);
        return Ok();
    }



    [HttpPost("CreateAuction")]
    [HaveAccess(Role.Seller)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAuction(CreateAuctionProductDto createProduct,
                                                CancellationToken cancellationToken)
    {
        await _productAppService.CreateAuction(createProduct, cancellationToken);
        return Ok();
    }


    [HttpPut("UpdateNonAuction/{productId}")]
    [HaveAccess(Role.Admin, Role.Seller)]
    public async Task<IActionResult> UpdateNonAuction(int productId,
                                                        UpdateNonAuctionProductDto productDto,
                                                        CancellationToken cancellationToken)
    {
        await _productAppService.UpdateNonAuction(productId, productDto, cancellationToken); 
        return Ok();
    }

    [HttpPut("UpdateAuction/{productId}")]
    [HaveAccess(Role.Admin, Role.Seller)]
    public async Task UpdateAuction(int productId, UpdateAuctionProductDto productDto,
                                     CancellationToken cancellationToken)
    {
        await _productAppService.UpdateAuction(productId, productDto, cancellationToken);
    }



    [HttpDelete("Remove/{productId}")]
    [HaveAccess(Role.Admin, Role.Seller)]
    public async Task<IActionResult> Remove(int productId,
                                            CancellationToken cancellationToken)
    {
        await _productAppService.Remove(productId, cancellationToken);
        return Ok();
    }
}
