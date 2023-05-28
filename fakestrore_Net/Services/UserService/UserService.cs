using fakestrore_Net.Data;
using fakestrore_Net.DTOs;
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
                ProductId = request.ProductId,
                TotalAmount = request.TotalAmount
            };
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            return await _context.Orders.ToListAsync();

            /*             var categoryId = request.CategoryID;
                        var existingCategory = await _context.Categories.FindAsync(categoryId);

                        if (existingCategory == null)
                        {
                            return null;
                        }
                        var newProduct = new Product
                        {
                            Title = request.Title,
                            Price = request.Price,
                            Description = request.Description,
                            Image = request.Image,
                            CategoryID = categoryId,
                            Rating = new Rating
                            {
                                Rate = request.Rating.Rate,
                                Count = request.Rating.Count
                            }
                        };

                        _context.Products.Add(newProduct);
                        await _context.SaveChangesAsync();
                        return await _context.Products.ToListAsync();*/
        }
    }
}
