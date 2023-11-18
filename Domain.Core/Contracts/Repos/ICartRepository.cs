using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Cart;
using Domain.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core.Contracts.Repos;

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

    Task AddAuctionOrder(int? customerId,
                         int productId, decimal proposedPrice, bool isCommit,
                         CancellationToken cancellationToken);

    Task DeleteOrder(int orderId,
                     CancellationToken cancellationToken);


    Task<CartStatus> GetCartStatus(int cartId,
                       CancellationToken cancellationToken);


    Task FinalizeCart(int cartId, CancellationToken cancellationToken);



    Task CancelCart(int cartId, CancellationToken cancellationToken);

    Task<List<OrderWithProductDto>> GetOrdersInCart(int cartId,
                                      CancellationToken cancellationToken);

    Task<bool> HasCustomerBuyed(int customerId, int productId,
                                      CancellationToken cancellationToken);
}
