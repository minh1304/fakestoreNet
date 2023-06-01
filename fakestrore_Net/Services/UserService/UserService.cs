using fakestrore_Net.Data;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.UserDTO;
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
        public async Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request)
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

            var newOrder = new Order
            {
                UserId = int.Parse(userId),
                OrderDate = DateTime.Now,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var orderProductDto in request.OrderProduct)
            {
                var existingProduct = await _context.Products.FindAsync(orderProductDto.ProductId);
                if (existingProduct != null)
                {
                    var orderProduct = new OrderProduct
                    {
                        Product = existingProduct,
                        Quantity = orderProductDto.Quantity
                    };
                    newOrder.OrderProducts.Add(orderProduct);
                }
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            var orders = await _context.Orders.ToListAsync();
            return orders; // Trả về danh sách đơn hàng
        }

        public async Task<ActionResult<UserGetDTO>> Information()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new ActionResult<UserGetDTO>(new BadRequestResult()); // Người dùng chưa đăng nhập, trả về mã lỗi 401
            }
            var existingUser = await _context.Users.FindAsync(int.Parse(userId));

            if (existingUser == null)
            {
                return new ActionResult<UserGetDTO>(new NotFoundResult()); // Người dùng không tồn tại, trả về mã lỗi 404
            }

            var userDto = new UserGetDTO
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                UserEmail = existingUser.UserEmail,
                Role = existingUser.Role

                // Các thuộc tính khác của đối tượng UserGetDTO
            };

            return userDto;

        }
    }
}