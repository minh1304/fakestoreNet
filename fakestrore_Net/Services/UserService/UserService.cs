using fakestrore_Net.Data;
using fakestrore_Net.DTOs.CartDTO;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.ProductDTO;
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
        private int? GetAuthenticatedUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int id))
            {
                return id;
            }
            return null;

        }
        public async Task<ActionResult<List<Cart>>> AddCart(CartCreateDTO request)
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
                CartDate = DateTime.Now,
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

        public async Task<ActionResult<List<CartGetDTO>>> GetCart()
        {
            var userId = GetAuthenticatedUserId();
            if (userId == null)
            {
                return null;
            }
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return null;
            }
            var carts = await _context.Carts
                .Include(c => c.CartProducts)
                .ThenInclude(cp => cp.Product)
                .Where(c => c.UserId == existingUser.Id)
                .ToListAsync();
            var cartGetDTO = carts.Select(carts => new CartGetDTO
            {
                Id = carts.Id,
                UserId = carts.UserId,
                TotalPrice = carts.CartProducts
                                    .Where(orderProduct => orderProduct.Product != null)
                                    .Sum(orderProduct => orderProduct.Quantity * orderProduct.Product.Price),
                Products = carts.CartProducts
                                    .Select(orderProduct => new ProductGetDTO
                                    {
                                        Id = orderProduct.Product != null ? orderProduct.Product.Id : 0,
                                        Title = orderProduct.Product != null ? orderProduct.Product.Title : string.Empty,
                                        Price = orderProduct.Product != null ? orderProduct.Product.Price : 0,
                                        Image = orderProduct.Product != null ? orderProduct.Product.Image : string.Empty,
                                        Quantity = orderProduct.Quantity
                                    })
                                    .ToList()
            }).ToList();
            return cartGetDTO;
        }

        public async Task<ActionResult<List<Order>>> AddOrder(OrderCreateDTO request)
        {
            var userId = GetAuthenticatedUserId();
            if (userId == null)
            {
                return null;
            }
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return null;
            }

            var newOrder = new Order
            {
                CustomerName = request.CustomerName,
                Address = request.Address,
                UserId = existingUser.Id,
                Carts = new List<Cart>()
            };

            decimal totalPrice = 0; // Biến để tính tổng giá tiền

            foreach (var cartDto in request.Carts)
            {
                var newCart = new Cart
                {
                    UserId = existingUser.Id,
                    CartDate = DateTime.Now,
                    CartProducts = new List<CartProduct>()
                };

                foreach (var cartProductDto in cartDto.CartProduct)
                {
                    var existingProduct = await _context.Products.FindAsync(cartProductDto.ProductId);
                    if (existingProduct != null)
                    {
                        var cartProduct = new CartProduct
                        {
                            Product = existingProduct,
                            Quantity = cartProductDto.Quantity
                        };
                        newCart.CartProducts.Add(cartProduct);
                        totalPrice += cartProduct.Quantity * cartProduct.Product.Price; // Tính tổng giá tiền
                    }
                }

                newOrder.Carts.Add(newCart);
            }

            newOrder.TotalPrice = totalPrice; // Gán giá trị tổng giá tiền cho đơn hàng

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            var orders = await _context.Orders.ToListAsync();
            return orders;
        }
    }
}