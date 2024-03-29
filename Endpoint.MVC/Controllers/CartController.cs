﻿using Endpoint.MVC.Dtos.Cart;
using Endpoint.MVC.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Endpoint.MVC.Controllers;

public class CartController : BaseController
{
    public CartController(IHttpClientFactory httpClientFactory)
                                            : base(httpClientFactory)
    { }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GetInProgressCart(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Cart/GetCurrentCartByCustomerId", cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        var currentCart = await httpResponseMessage.Content.ReadFromJsonAsync<CartDto>();
        var httpResponseMessage2 = await SendGetRequest($"Cart/GetDetailsByCartId/{currentCart.Id}",
                                                        cancellationToken);

        if (!httpResponseMessage2.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage2);

        var currentCartDetail = await httpResponseMessage2.Content.ReadFromJsonAsync<CartDetailsDto>();

        return View(currentCartDetail);
    }



    public async Task<IActionResult> GetAllByCustomerId(CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendGetRequest("Cart/GetByCustomerId",
                                                        cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);
        var carts = await httpResponseMessage.Content.ReadFromJsonAsync<List<CartDto>>();
        return View(carts);
    }



    public async Task<IActionResult> DeleteOrder(int orderId,
                                                 CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendDeleteRequest($"cart/DeleteOrder/{orderId}",
                                                          cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetInProgressCart");
    }

    public async Task<IActionResult> FinalizeCart(int cartId,
                                                  CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPatchRequest($"cart/FinalizeCart/{cartId}", 
                                                                     cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetAllByCustomerId");
    }

    public async Task<IActionResult> CancelCart(int cartId,
                                               CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPatchRequest($"cart/CancelCart/{cartId}",
                                                         cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetAllByCustomerId");
    }


    [HttpPost]
    public async Task<IActionResult> AddAuctionOrder(AddAuctionOrderDto auctionOrder, 
                                                     CancellationToken cancellationToken)
    {
        var httpResponseMessage = await SendPostRequest($"cart/AddAuctionOrder",
                                                        JsonConvert.SerializeObject(auctionOrder),
                                                         cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
            return RedirectToErrorPage(httpResponseMessage);

        return RedirectToAction("GetNonAuctions", "Product");
    }

}
