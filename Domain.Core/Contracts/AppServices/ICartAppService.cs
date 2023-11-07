using Domain.Core.Dtos.Cart;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.AppServices;


public interface ICartAppService
{
    Task AddNonAuctionProductToCart(int customerId,
                          int productId,
                          int count,
                          CancellationToken cancellationToken);


    Task<List<CartDto>> GetByCartStatus(int customerId,
                                        CartStatus cartStatus,
                                        CancellationToken cancellationToken);

    Task<List<CartDto>> GetAllByCustomerId(int customerId,
                                           CancellationToken cancellationToken);

    Task AddAuctionOrder(int customerId,
                         int productId, decimal proposedPrice,
                         CancellationToken cancellationToken);

    Task DeleteOrder(int orderId,
                     CancellationToken cancellationToken);


    Task<CartDetailsDto> GetDetailsByCartId(int cartId,
                                            CancellationToken cancellationToken);

    Task ChangeCartStatus(int cartId,
                          CartStatus cartStatus,
                          CancellationToken cancellationToken);
}


