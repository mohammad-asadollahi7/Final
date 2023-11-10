﻿using Domain.Core.Contracts.AppServices;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Cart;
using Domain.Core.Enums;

namespace Domain.AppServices;

public class CartAppService : ICartAppService
{
    private readonly ICartService _cartService;
    private readonly IProductService _productService;

    public CartAppService(ICartService cartService, IProductService productService)
    {
        _cartService = cartService;
        _productService = productService;
    }
    public async Task AddNonAuctionProductToCart(int customerId,
                                                int productId,
                                                int quantity,
                                                CancellationToken cancellationToken)
    {
        await _productService.EnsureProductQuantitySufficient(productId,
                                                              quantity,
                                                              cancellationToken);

        var cartId = await _cartService.CreateByCustomerId(customerId,
                                                           cancellationToken);

        var product = await _productService.GetNonAuctionProductById(productId, true, cancellationToken);

        await _cartService.AddOrder(cartId,
                                    productId,
                                    quantity,
                                    product.Price,
                                    product.DiscountPercent ?? 0,
                                    cancellationToken);
    }

    public async Task AddAuctionOrder(int customerId,
                                      int productId, decimal proposedPrice,
                                      CancellationToken cancellationToken)
    {
        var product = await _productService.GetAuctionProductById(productId, true, cancellationToken);

        await _cartService.AddAuctionOrder(customerId, productId, product.Price,
                                           proposedPrice, true, cancellationToken);
    }

    public async Task DeleteOrder(int orderId,
                                  CancellationToken cancellationToken)
    {
        await _cartService.DeleteOrder(orderId, cancellationToken);
    }


    public async Task<List<CartDto>> GetAllByCustomerId(int customerId,
                                                        CancellationToken cancellationToken)
    {
        return await _cartService.GetAllByCustomerId(customerId, cancellationToken);
    }


    public async Task<List<CartDto>> GetByCartStatus(int customerId,
                                                     CartStatus cartStatus,
                                                     CancellationToken cancellationToken)
    {
        return await _cartService.GetByCartStatus(customerId, cartStatus, cancellationToken);
    }


    public async Task<CartDetailsDto> GetDetailsByCartId(int cartId,
                                                         CancellationToken cancellationToken)
    {
        return await _cartService.GetDetailsByCartId(cartId, cancellationToken);
    }

    public async Task FinalizeCart(int cartId, CancellationToken cancellationToken)
    {
        await _cartService.CheckCartStatus(cartId, CartStatus.In_Progress, cancellationToken);
        await _cartService.FinalizeCart(cartId, cancellationToken);
    }


    public async Task CancelCart(int cartId, CancellationToken cancellationToken)
    {
        await _cartService.CheckCartStatus(cartId, CartStatus.In_Progress, cancellationToken);
        await _cartService.CancelCart(cartId, cancellationToken);
    }
}
