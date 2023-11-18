using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Booth;
using Domain.Core.Dtos.Cart;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Infra.Db.EF;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Infra.DataAccess.Repos.EF;


public class CartRepository : ICartRepository
{
    private readonly FinalContext _context;

    public CartRepository(FinalContext context) => _context = context;


    public async Task<List<CartDto>> GetAllByCustomerId(int customerId,
                                                 CancellationToken cancellationToken)
    {
        var cartDtos = await _context.Carts.Where(c => c.CustomerId == customerId)
                                           .Select(c => new CartDto()
                                           {
                                               Id = c.Id,
                                               CartStatus = c.Status,
                                               OrderAt = c.OrderAt
                                           })
                                           .AsNoTracking()
                                           .ToListAsync(cancellationToken);
        return cartDtos;
    }


    public async Task<CartDetailsDto?> GetDetailsByCartId(int cartId,
                                                         CancellationToken cancellationToken)
    {
        var cartDetailDto = await _context.Carts.Where(c => c.Id == cartId)
                                                .Select(c => new CartDetailsDto()
                                                {
                                                    Id = c.Id,
                                                    CartStatus = c.Status,
                                                    CustomerId = c.CustomerId,
                                                    OrderAt = c.OrderAt,
                                                    OrderDtos = c.Orders.Select(o => new OrderDto()
                                                    {
                                                        Id = o.Id,
                                                        Quantity = o.Quantity,
                                                        DiscountedPrice = o.DiscountedPrice,
                                                        ProductDto = new ProductInOrderDto()
                                                        {
                                                            Id = o.Id,
                                                            PersianTitle = o.Product.PersianTitle,
                                                            PictureName = o.Product.ProductPictures
                                                                            .Select(pp => pp.Picture.Name)
                                                                            .FirstOrDefault()
                                                        }
                                                    }).ToList()
                                                }).AsNoTracking()
                                                .FirstOrDefaultAsync(cancellationToken);
        return cartDetailDto;
    }


    public async Task<List<CartDto>> GetByCartStatus(int customerId,
                                                     CartStatus cartStatus,
                                                     CancellationToken cancellationToken)
    {
        return await _context.Carts.Where(c => c.CustomerId == customerId
                                          && c.Status == cartStatus)
                                          .Select(c => new CartDto()
                                          {
                                              Id = c.Id,
                                              CartStatus = c.Status,
                                              OrderAt = c.OrderAt
                                          }).ToListAsync();
    }


    public async Task<CartStatus> GetCartStatus(int cartId, CancellationToken cancellationToken)
    {
        return await _context.Carts.Where(c => c.Id == cartId)
                                    .Select(c => c.Status).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task FinalizeCart(int cartId, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId,
                                                            cancellationToken);
        cart.Status = CartStatus.Delivered;
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task CancelCart(int cartId, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId,
                                                            cancellationToken);
        cart.Status = CartStatus.cancelled;
        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task AddOrder(int cartId,
                               int productId,
                               int quantity,
                               decimal discountedPrice,
                               CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.Where(c => c.Id == cartId)
                                .Include(c => c.Orders)
                                .SingleOrDefaultAsync(cancellationToken);

        var sameOrder = cart.Orders.FirstOrDefault(o => o.ProductId == productId);
        if (sameOrder is not null)
        {
            sameOrder.Quantity += quantity;
        }

        else
        {
            var newOrder = new Order()
            {
                CartId = cartId,
                DiscountedPrice = discountedPrice,
                ProductId = productId,
                Quantity = quantity,
                BuyType = 0,
            };
            cart.Orders.Add(newOrder);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task DeleteOrder(int orderId,
                                  CancellationToken cancellationToken)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == orderId,
                                                               cancellationToken);
        _context.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
    }



    public async Task<int> CreateByCustomerId(int customerId,
                                              CancellationToken cancellationToken)
    {
        var newCart = new Cart()
        {
            OrderAt = DateTime.Now,
            Status = CartStatus.In_Progress,
            CustomerId = customerId,
        };

        await _context.Carts.AddAsync(newCart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var newCartId = _context.Entry(newCart)
                                .Property(c => c.Id).CurrentValue;
        return newCartId;
    }

    public async Task AddAuctionOrder(int? customerId,
                                      int productId, decimal proposedPrice,
                                      bool isCommit,
                                      CancellationToken cancellationToken)
    {
        var auctionOrder = new AuctionOrder()
        {
            Price = proposedPrice,
            CustomerId = customerId,
            ProductId = productId,
        };

        await _context.AuctionOrders.AddAsync(auctionOrder, cancellationToken);

        if (isCommit)
            await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<OrderWithProductDto>> GetOrdersInCart(int cartId,
                                                    CancellationToken cancellationToken)
    {
        return await _context.Orders.Where(o => o.CartId == cartId)
                                          .Select(o => new OrderWithProductDto()
                                          {
                                              DiscountedPrice = o.DiscountedPrice,
                                              Quantity = o.Quantity,
                                              BoothId = o.Product.BoothId,
                                              ProductId = o.ProductId,
                                              Wage = o.Product.Booth.Wage
                                          }).ToListAsync(cancellationToken);
    }


    public async Task<bool> HasCustomerBuyed(int customerId, int productId, 
                                      CancellationToken cancellationToken)
    {
        return await _context.Orders.Where(o => o.Cart.CustomerId == customerId)
                           .AnyAsync(o => o.ProductId == productId, cancellationToken);
    }
}
