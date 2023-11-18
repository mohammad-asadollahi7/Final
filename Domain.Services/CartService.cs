using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Cart;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Exceptions;

namespace Domain.Services;


public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, 
                        IProductService productService,
                        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productService = productService;
        _productRepository = productRepository;
    }

    public async Task AddAuctionOrder(int? customerId, int productId,
                                       decimal MinPrice, decimal proposedPrice,
                                       bool isCommit,
                                       CancellationToken cancellationToken)
    {
        if (MinPrice > proposedPrice)
            throw new AppException(ExpMessage.LowPrice,
                                   ExpStatusCode.BadRequest);


        await _cartRepository.AddAuctionOrder(customerId, productId,
                                        proposedPrice, isCommit, cancellationToken);
    }

    public async Task AddOrder(int cartId,
                               int productId,
                               int quantity,
                               decimal price,
                               int discount,
                               CancellationToken cancellationToken)
    {
        decimal discountedPrice;
        discountedPrice = ((100 - discount) * 0.01m) * price;

        await _cartRepository.AddOrder(cartId, productId, quantity,
                                       discountedPrice, cancellationToken);
    }


    public async Task<int> CreateByCustomerId(int customerId,
                                              CancellationToken cancellationToken)
    {
        var carts = await _cartRepository.GetByCartStatus(customerId,
                                                          CartStatus.In_Progress,
                                                          cancellationToken);

        if (carts.Count() == 0)
        {
            var newCartId = await _cartRepository.CreateByCustomerId(customerId,
                                                                    cancellationToken);
            return newCartId;
        }
        return carts.Select(c => c.Id).First();

    }

    public async Task DeleteOrder(int orderId,
                                  CancellationToken cancellationToken)
    {
        await _cartRepository.DeleteOrder(orderId, cancellationToken);
    }



    public async Task<List<CartDto>> GetAllByCustomerId(int customerId,
                                                        CancellationToken cancellationToken)
    {
        var cartDtos = await _cartRepository.GetAllByCustomerId(customerId, cancellationToken);
        if (cartDtos.Count() == 0)
            throw new AppException(ExpMessage.NotExistCarts,
                                   ExpStatusCode.NotFound);

        return cartDtos;
    }

    public async Task<List<CartDto>> GetByCartStatus(int customerId,
                                                     CartStatus cartStatus,
                                                     CancellationToken cancellationToken)
    {
        var cartDtos = await _cartRepository.GetByCartStatus(customerId,
                                                             cartStatus,
                                                             cancellationToken);
        if (cartDtos.Count() == 0)
            throw new AppException(ExpMessage.NotExistCart,
                                   ExpStatusCode.NotFound);
        return cartDtos;
    }

    public async Task<CartDetailsDto> GetDetailsByCartId(int cartId,
                                                         CancellationToken cancellationToken)
    {
        var cartDto = await _cartRepository.GetDetailsByCartId(cartId, cancellationToken);
        if (cartDto is null)
            throw new AppException(ExpMessage.NotExistCart,
                                   ExpStatusCode.NotFound);
        return cartDto;
    }


    public async Task CheckCartStatus(int cartId,
                                      CartStatus initialCartStatus,
                                      CancellationToken cancellationToken)
    {
        var cartStauts = await _cartRepository.GetCartStatus(cartId, cancellationToken);
        if (cartStauts != initialCartStatus)
            throw new AppException(ExpMessage.WrongCartStatus, ExpStatusCode.Conflict);
    }


    public async Task FinalizeCart(int cartId, CancellationToken cancellationToken)
    {
        await _cartRepository.FinalizeCart(cartId, cancellationToken);
    }


    public async Task CancelCart(int cartId, CancellationToken cancellationToken)
    {
        await _cartRepository.CancelCart(cartId, cancellationToken);
    }

    public async Task<List<OrderWithProductDto>> GetOrdersInCart(int cartId,
                                                    CancellationToken cancellationToken)
    {
        return await _cartRepository.GetOrdersInCart(cartId, cancellationToken);
    }

    public async Task EnsureCustomerBuyed(int customerId, int productId,
                                      CancellationToken cancellationToken)
    {
        var hasBuyed = await _cartRepository.HasCustomerBuyed(customerId, 
                                            productId, cancellationToken);
        if (!hasBuyed)
            throw new AppException(ExpMessage.HasNotBuyed, ExpStatusCode.BadRequest);
    }


}
