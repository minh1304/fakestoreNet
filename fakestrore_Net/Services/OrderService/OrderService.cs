using fakestrore_Net.Data;
using fakestrore_Net.DTOs;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace fakestrore_Net.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetAuthenticatedUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int id))
            {
                return id;
            }
            return 0;

        }
        public async Task<string> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");

            var order = new Order
            {
                UserId = userId,
                Name = orderCreateDto.Name,
                PhoneNumber = orderCreateDto.PhoneNumber,
                Address = orderCreateDto.Address,
                Note = orderCreateDto.Note,
                OrderDate = DateTime.UtcNow,
                OrderUpdatedDate = DateTime.UtcNow,
                IsActive = "Y",
                Status = "Pending",
                OrderProducts = orderCreateDto.Products.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return "Success";
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync()
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.UserId == userId && o.IsActive == "Y")
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = userId,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    Note = o.Note,
                    Name = o.Name,
                    Status = o.Status,
                    OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Title = op.Product.Title,
                        Price = op.Product.Price,
                        Image = op.Product.Image
                    }).ToList()
                })
                .OrderByDescending(o => o.OrderUpdatedDate)
                .ToListAsync();

            return orders;
        }

        public async Task<OrderDto> GetOrderByOrderIdAsync(int orderId)
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.UserId == userId && o.IsActive == "Y" && o.Id == orderId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = userId,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    Note = o.Note,
                    Name = o.Name,
                    Status = o.Status,
                    OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Title = op.Product.Title,
                        Price = op.Product.Price,
                        Image = op.Product.Image
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            return order;
        }

        public async Task<bool> CancelOrderByIdAsync(int orderId)
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return false;
            }
            order.IsActive = "N";
            order.Status = "Canceled";
            order.OrderUpdatedDate = DateTime.UtcNow;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateOrderById(int orderId)
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return false;
            }
            order.Status = "Completed";
            order.IsActive = "N";
            order.OrderUpdatedDate = DateTime.UtcNow;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrderDto>> GetOrdersCompletedAsync()
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.UserId == userId && o.IsActive == "N" && o.Status == "Completed")
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = userId,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    Note = o.Note,
                    Name = o.Name,
                    Status = o.Status,
                    OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Title = op.Product.Title,
                        Price = op.Product.Price,
                        Image = op.Product.Image
                    }).ToList()
                })
                .OrderByDescending(o => o.OrderUpdatedDate)
                .ToListAsync();

            return orders;
        }

        public async Task<List<OrderDto>> GetOrdersCanceledAsync()
        {
            var userId = GetAuthenticatedUserId();
            if (userId == 0)
                throw new Exception("User not found");
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.UserId == userId && o.IsActive == "N" && o.Status == "Canceled")
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = userId,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address,
                    Note = o.Note,
                    Name = o.Name,
                    Status = o.Status,
                    OrderProducts = o.OrderProducts.Select(op => new OrderProductDto
                    {
                        ProductId = op.ProductId,
                        Quantity = op.Quantity,
                        Title = op.Product.Title,
                        Price = op.Product.Price,
                        Image = op.Product.Image
                    }).ToList()
                })
                .OrderByDescending(o => o.OrderUpdatedDate)
                .ToListAsync();

            return orders;
        }
    }
}
