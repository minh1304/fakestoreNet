using fakestrore_Net.Data;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.DTOs.UserDTO;
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
                return null;
            }

            var newOrder = new Order
            {
                UserId = request.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = request.TotalAmount,
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

        public async Task<ActionResult<List<UserGetDTO>>> GetUser()
        {
            var query = await _context.Users
               .Include(u => u.Orders)
                   .ThenInclude(o => o.Products)
               .ToListAsync();

            if (query.Count == 0)
            {
                return null; // Return appropriate HTTP status code for empty result
            }

            var result = query.Select(user => new UserGetDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                PasswordHash = user.PasswordHash,
                Orders = user.Orders
                    //Check mảng rỗng 
                    .Where(order => order.Products.Count > 0)
                    .Select(order => new OrderGetDTO
                    {
                        UserId = order.UserId,
                        //Chưa làm 
                        TotalAmount = order.TotalAmount,
                        Products = order.Products.Select(product => new ProductGetDTO
                        {
                            Id = product.Id,
                            Title = product.Title,
                            Price = product.Price,
                            Image = product.Image,
                        }).ToList()

                    })
                    .ToList()
            }).ToList();

            return result;
        }








        //Get order
        /*        public Task<ActionResult<List<User>>> GetOrder(User request)
                {

                }*/
    }
}
