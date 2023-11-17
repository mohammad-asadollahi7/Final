using Domain.Core.Contracts.AppServices;
using Domain.Core.Dtos.Comment;
using Domain.Core.Dtos.Product;
using Domain.Core.Dtos.Products;
using Domain.Core.Enums;
using Endpoint.API.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Endpoint.API.Controllers;
public class ProductController : BaseController
{
    private readonly IProductAppService _productAppService;
    private readonly ICommentAppService _commentAppService;

    public ProductController(IProductAppService productAppService,
                             ICommentAppService commentAppService)
    {
        _productAppService = productAppService;
        _commentAppService = commentAppService;
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
    public async Task<IActionResult> GetAuctionProductById(int productId, [FromQuery] bool? isApproved,
                                                           CancellationToken cancellationToken)
    {
        var product = await _productAppService.GetAuctionProductById(productId, isApproved, cancellationToken);
        return Ok(product);
    }


    [HttpGet("GetNonAuctionProductById/{productId}")]
    public async Task<IActionResult> GetNonAuctionProductById(int productId,[FromQuery] bool? isApproved,
                                                             CancellationToken cancellationToken)
    {
        var product = await _productAppService.GetNonAuctionProductById(productId, isApproved,
                                                                        cancellationToken);
        return Ok(product);
    }


    [HttpGet("GetProductsForApprove")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetProductsForApprove(
                                                    CancellationToken cancellationToken)
    {
        var products = await _productAppService.GetProductsForApprove(cancellationToken);
        return Ok(products);
    }


    [HttpPost("CreateNonAuction")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> CreateNonAuction(CreateNonAuctionProductDto createProduct,
                                       CancellationToken cancellationToken)
    {
        await _productAppService.CreateNonAuction(CurrentUserId, createProduct, cancellationToken);
        return Ok();
    }



    [HttpPost("CreateAuction")]
    //[HaveAccess(Role.Seller)]
    public async Task<IActionResult> CreateAuction(CreateAuctionProductDto createProduct,
                                                CancellationToken cancellationToken)
    {
        await _productAppService.CreateAuction(CurrentUserId, createProduct, cancellationToken);
        return Ok();
    }


    [HttpPut("UpdateNonAuction/{productId}")]
    //[HaveAccess(Role.Admin, Role.Seller)]
    public async Task<IActionResult> UpdateNonAuction(int productId,
                                                      UpdateNonAuctionProductDto productDto,
                                                      CancellationToken cancellationToken)
    {
        await _productAppService.UpdateNonAuction(productId, productDto, cancellationToken); 
        return Ok();
    }

    [HttpPut("UpdateAuction/{productId}")]
    //[HaveAccess(Role.Admin, Role.Seller)]
    public async Task UpdateAuction(int productId, AuctionDetailsDto productDto,
                                     CancellationToken cancellationToken)
    {
        await _productAppService.UpdateAuction(productId, productDto, cancellationToken);
    }



    [HttpDelete("Remove/{productId}")]
   // [HaveAccess(Role.Admin, Role.Seller)]
    public async Task<IActionResult> Remove(int productId,
                                            CancellationToken cancellationToken)
    {
        await _productAppService.Remove(productId, cancellationToken);
        return Ok();
    }


    [HttpPatch("ApproveProduct/{id}/{isApproved}")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> ApproveProduct(int id, bool isApproved,
                                   CancellationToken cancellationToken)
    {
        await _productAppService.ApproveProduct(id, isApproved, cancellationToken);
        return Ok();
    }

    [HttpGet("GetAllComments")]
   // [HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetAllComments([FromQuery] bool? isApproved, 
                                                    CancellationToken cancellationToken)
    {
        var comments = await _commentAppService.GetAllComments(isApproved, cancellationToken);
        return Ok(comments);
    }


    [HttpPatch("ApproveComment/{id}")]
    //[HaveAccess(Role.Admin)]
    public async Task ApproveComment(int id, [FromQuery] bool isApproved, 
                                     CancellationToken cancellationToken)
    {
        await _commentAppService.ApproveComment(id, isApproved, cancellationToken); 
    }


    [HttpGet("GetWages")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetWages(CancellationToken cancellationToken)
    {
        var wages = await _productAppService.GetWages(cancellationToken);
        return Ok(wages);
    }


    [HttpGet("GetNumberOfProductsForApprove")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetNumberOfProductsForApprove(CancellationToken cancellationToken)
    {
        var number = await _productAppService.GetNumberOfProductsForApprove(cancellationToken);
        return Ok(number);
    }


    [HttpGet("GetNumberOfCommentsForApprove")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetNumberOfCommentsForApprove(CancellationToken cancellationToken)
    {
        var number = await _commentAppService.GetNumberOfCommentsForApprove(cancellationToken);
        return Ok(number);
    }


    [HttpGet("GetWageNumbers")]
    //[HaveAccess(Role.Admin)]
    public async Task<IActionResult> GetWageNumbers(CancellationToken cancellationToken)
    {
        var numberOfWages = await _productAppService.GetWageNumbers(cancellationToken);
        return Ok(numberOfWages);
    }



    [HttpGet("GetAuctions")]
    //[HaveAccess(Role.Admin, Role.Customer)]
    public async Task<IActionResult> GetAuctions(CancellationToken cancellationToken,
                                                [FromQuery] bool? isApproved)
    {
        var products = await _productAppService.GetAuctions(isApproved, cancellationToken);
        return Ok(products);
    }


    [HttpGet("GetCommentsByProductId/{productId}")]
    public async Task<IActionResult> GetCommentsByProductId(int productId,
                                            CancellationToken cancellationToken)
    {
        var comments = await _commentAppService.GetCommentsByProductId(productId, 
                                                             cancellationToken);
        return Ok(comments);
    }
}

