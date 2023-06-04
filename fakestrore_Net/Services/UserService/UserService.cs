using fakestrore_Net.Data;
using fakestrore_Net.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fakestrore_Net.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ActionResult<List<Cart>>> AddOrder(CartCreateDTO request)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return null; // Người dùng chưa đăng nhập, trả về mã lỗi 401
            }
            var existingUser = await _context.Users.FindAsync(int.Parse(userId));
            if (existingUser == null)
            {
                return null; // Return appropriate HTTP status code for user not found
            }

            var newOrder = new Cart
            {
                UserId = int.Parse(userId),
                OrderDate = DateTime.Now,
                CartProducts = new List<CartProduct>()
            };

            foreach (var cartProductDto in request.CartProduct)
            {
                var existingProduct = await _context.Products.FindAsync(cartProductDto.ProductId);
                if (existingProduct != null)
                {
                    var orderProduct = new CartProduct
                    {
                        Product = existingProduct,
                        Quantity = cartProductDto.Quantity
                    };
                    newOrder.CartProducts.Add(orderProduct);
                }
            }

            _context.Carts.Add(newOrder);
            await _context.SaveChangesAsync();

            var orders = await _context.Carts.ToListAsync();
            return orders; // Trả về danh sách đơn hàng
        }


    }
}