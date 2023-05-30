using fakestrore_Net.Data;
using fakestrore_Net.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request)
        {
            var userId = request.UserId;
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return null; // Return appropriate HTTP status code for user not found
            }

            var newOrder = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.Now,
                OrderProducts = new List<OrderProduct>()
            };

            var existingProduct = await _context.Products.FindAsync(request.OrderProduct.ProductId);
            if (existingProduct != null)
            {
                var orderProduct = new OrderProduct
                {
                    Product = existingProduct,
                    Quantity = request.OrderProduct.Quantity
                };
                newOrder.OrderProducts.Add(orderProduct);
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return await _context.Orders.ToListAsync();
        }

    }
}