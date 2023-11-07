using Domain.Core.Contracts.Repos;
using Domain.Core.Contracts.Services;
using Domain.Core.Dtos.Cart;
using Domain.Core.Enums;
using Domain.Core.Exceptions;

namespace Domain.Services;


public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductService _productService;

    public CartService(ICartRepository cartRepository, IProductService productService)
    {
        _cartRepository = cartRepository;
        _productService = productService;
        
    }

    public async Task AddAuctionOrder(int? customerId, int productId, 
                                       decimal lastSubmittedPrice, decimal proposedPrice, 
                                       CancellationToken cancellationToken)
    {
        if (lastSubmittedPrice < proposedPrice)
            throw new AppException(ExpMessage.LowPrice,   
                                     ExpStatusCode.BadRequest);


        await _cartRepository.AddAuctionOrder(customerId, productId,
                                                proposedPrice, cancellationToken);
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

}
