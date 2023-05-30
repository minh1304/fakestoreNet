using fakestrore_Net.Data;
using fakestrore_Net.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request)
        {
            throw new NotImplementedException();
        }
        /* public async Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request)
{
    var userId = request.UserId;
    var existingUser = await _context.Users.FindAsync(userId);
    if (existingUser == null)
    {
        return null;
    }

    var newOrder = new Order
    {
        UserId = request.UserId,
        OrderDate = DateTime.Now,
        Products = new List<Product>()
    };

    foreach (var productId in request.ProductIds)
    {
        var existingProduct = await _context.Products.FindAsync(productId);
        if (existingProduct != null)
        {
            newOrder.Products.Add(existingProduct);
        }
    }

    _context.Orders.Add(newOrder);
    await _context.SaveChangesAsync();

    return await _context.Orders.ToListAsync();
}
*/

    }
}
