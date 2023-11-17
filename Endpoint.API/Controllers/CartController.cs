using Domain.Core.Contracts.AppServices;
using Domain.Core.Enums;
using Endpoint.API.CustomAttributes;
using Endpoint.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.API.Controllers;

public class CartController : BaseController
{
    private readonly ICartAppService _cartAppService;

    public CartController(ICartAppService cartAppService)
    {
        _cartAppService = cartAppService;
    }



    [HttpGet("AddProduct/{productId}")]
    //[HaveAccess(Role.Customer)]
    public async Task<IActionResult> AddProductToCart(int productId, 
                                                CancellationToken cancellationToken)
    {
        await _cartAppService.AddNonAuctionProductToCart(CurrentUserId,
                                                         productId,
                                                         1,
                                                         cancellationToken);
        return Ok();
    }



    [HttpGet("GetByStatus")]
    //[HaveAccess(Role.Customer, Role.Admin)]
    public async Task<IActionResult> GetByCartStatus(CartStatus cartStatus,
                                                     CancellationToken cancellationToken)
    {
        var carts = await _cartAppService.GetByCartStatus(CurrentUserId,
                                                          cartStatus,
                                                          cancellationToken);
        return Ok(carts);
    }



    [HttpGet("GetCurrentCartByCustomerId")]
   // [HaveAccess(Role.Customer)]
    public async Task<IActionResult> GetCurrentCartByCustomerId(CancellationToken
                                                                    cancellationToken)
    {
        var carts = await _cartAppService.GetByCartStatus(CurrentUserId,
                                              CartStatus.In_Progress,
                                              cancellationToken);
        return Ok(carts.First());
    }



    [HttpGet("GetByCustomerId")]
   // [HaveAccess(Role.Customer)]
    public async Task<IActionResult> GetAllByCustomerId(CancellationToken cancellationToken)
    {
        var carts = await _cartAppService.GetAllByCustomerId(CurrentUserId, cancellationToken);
        return Ok(carts);
    }



    [HttpDelete("DeleteOrder/{orderId}")]
   // [HaveAccess(Role.Customer)]
    public async Task<IActionResult> DeleteOrder(int orderId,
                                                 CancellationToken cancellationToken)
    {
        await _cartAppService.DeleteOrder(orderId, cancellationToken);
        return Ok();
    }



    [HttpGet("GetDetailsByCartId/{cartId}")]
   // [HaveAccess(Role.Customer)]
    public async Task<IActionResult> GetDetailsByCartId(int cartId,
                                                        CancellationToken cancellationToken)
    {
        var cart = await _cartAppService.GetDetailsByCartId(cartId, cancellationToken);
        return Ok(cart);
    }


    [HttpPatch("FinalizeCart/{cartId}")]
    //[HaveAccess(Role.Customer, Role.Admin)]
    public async Task<IActionResult> FinalizeCart(int cartId, CancellationToken cancellationToken)
    {
        await _cartAppService.FinalizeCart(cartId, cancellationToken);
        return Ok();    
    }

    [HttpPatch("CancelCart/{cartId}")]
   // [HaveAccess(Role.Customer, Role.Admin)]
    public async Task<IActionResult> CancelCart(int cartId, CancellationToken cancellationToken)
    {
        await _cartAppService.CancelCart(cartId, cancellationToken);
        return Ok();
    }



    [HttpPatch("AddAuctionOrder/{productId}")]
  //  [HaveAccess(Role.Customer)]
    public async Task AddAuctionOrder(int productId, decimal proposedPrice,
                                      CancellationToken cancellationToken)
    {
        await _cartAppService.AddAuctionOrder(CurrentUserId, productId,
                                              proposedPrice, cancellationToken);
    }

}
