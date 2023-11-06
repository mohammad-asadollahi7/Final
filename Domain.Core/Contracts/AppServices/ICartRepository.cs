
using Domain.Core.Dtos.Cart;
using Domain.Core.Enums;

namespace Domain.Core.Contracts.AppServices;

public interface ICartRepository
{
    Task<List<CartDto>> GetAllByCustomerId(int customerId,
                                           CancellationToken cancellationToken);

    Task<CartDetailsDto?> GetDetailsByCartId(int cartId,
                                            CancellationToken cancellationToken);

    Task<List<CartDto>> GetByCartStatus(int cusotmerId,
                                        CartStatus cartStatus,
                                        CancellationToken cancellationToken);

    Task<int> CreateByCustomerId(int customerId,
                                 CancellationToken cancellationToken);

    Task AddOrder(int cartId,
                  int productId,
                  int quantity,
                  decimal discountedPrice,
                  CancellationToken cancellationToken);

    Task AddAuctionOrder(int customerId,
                         int productId, int proposedPrice,
                         CancellationToken cancellationToken);


    Task<bool> ChangeCartStatus(int cartId,
                                CartStatus cartStatus,
                                CancellationToken cancellationToken);

    Task DeleteOrder(int orderId,
                     CancellationToken cancellationToken);




}
