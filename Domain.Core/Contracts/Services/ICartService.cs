
using Domain.Core.Dtos.Cart;
using Domain.Core.Enums;
using System.ComponentModel;

namespace Domain.Core.Contracts.Services;


public interface ICartService
{
    Task<List<CartDto>> GetByCartStatus(int customerId,
                                        CartStatus cartStatus,
                                        CancellationToken cancellationToken);

    Task<List<CartDto>> GetAllByCustomerId(int customerId,
                                           CancellationToken cancellationToken);

    Task DeleteOrder(int orderId,
                     CancellationToken cancellationToken);

    Task<CartDetailsDto> GetDetailsByCartId(int cartId,
                                            CancellationToken cancellationToken);

    Task ChangeCartStatus(int cartId,
                          CartStatus cartStatus,
                          CancellationToken cancellationToken);


    Task<int> CreateByCustomerId(int customerId,
                                 CancellationToken cancellationToken);


    Task AddOrder(int cartId,
                  int productId,
                  int quantity,
                  decimal price,
                  int discount,
                  CancellationToken cancellationToken);

    Task AddAuctionOrder(int? customerId,
                         int productId, decimal proposedPrice,
                         CancellationToken cancellationToken);

}
