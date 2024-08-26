using fakestrore_Net.Data;
using fakestrore_Net.DTOs;
using fakestrore_Net.DTOs.CartDTO;
using fakestrore_Net.DTOs.CategoryDTO;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.DTOs.UserDTO;
using fakestrore_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;

        public AdminService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<Category>>> AddCategory(CategoryCreateDTO request)
        {
            var newCategory = new Category
            {
                Name = request.Name,
                Products = request.Products.Select(p => new Product
                {
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Image = p.Image,
                    AddedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsActive = "Y",
                    Rating = new Rating
                    {
                        Rate = p.Rating.Rate,
                        Count = p.Rating.Count
                    }
                }).ToList()
            };
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();
        }

        public async Task<ActionResult<List<Product>>> AddNewProduct(ProductCreateDTO request)
        {
            var categoryId = request.CategoryID;
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
                AddedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = "Y",
                Rating = new Rating
                {
                    Rate = request.Rating.Rate,
                    Count = request.Rating.Count
                }
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            product.IsActive = "N";
            product.UpdatedDate = DateTime.UtcNow; 
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<List<OrderDto>> GetAllOrderAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.IsActive == "Y")
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = o.UserId,
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
        public async Task<OrderResultDto> GetOrderByStatus(OrderGetDto request)
        {
            var query = _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.Address != "");

            // Apply filtering based on OrderId, Status, and date range
            if (!string.IsNullOrEmpty(request.SearchText) && int.TryParse(request.SearchText, out int orderId))
            {
                query = query.Where(o => o.Id == orderId);
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(o => o.Status == request.Status);
            }

            if (request.OrderDateFrom != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate >= request.OrderDateFrom);
            }

            if (request.OrderDateEnd != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate <= request.OrderDateEnd);
            }
            var totalOrders = await query.CountAsync();


            // Apply sorting and pagination
            var orders = await query
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = o.UserId,
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
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return new OrderResultDto
            {
                Orders = orders,
                TotalCount = totalOrders
            };
        }

        public async Task<ActionResult<List<UserGetDTO>>> GetAllUser()
        {
            var query = await _context.Users
                        .Include(u => u.Carts)
                            .ThenInclude(o => o.CartProducts)
                            .ThenInclude(op => op.Product)

                        .ToListAsync();

            if (query.Count == 0)
            {
                return null;
            }

            var result = query.Select(user => new UserGetDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                Role = user.Role,
                Carts = user.Carts

                            .Select(order => new CartGetDTO
                            {
                                Id = order.Id,
                                UserId = order.UserId,
                                TotalPrice = order.CartProducts
                                    .Where(orderProduct => orderProduct.Product != null)
                                    .Sum(orderProduct => orderProduct.Quantity * orderProduct.Product.Price),
                                Products = order.CartProducts
                                    .Select(orderProduct => new ProductGetDTO
                                    {
                                        Id = orderProduct.Product != null ? orderProduct.Product.Id : 0,
                                        Title = orderProduct.Product != null ? orderProduct.Product.Title : string.Empty,
                                        Price = orderProduct.Product != null ? orderProduct.Product.Price : 0,
                                        Image = orderProduct.Product != null ? orderProduct.Product.Image : string.Empty,
                                        Quantity = orderProduct.Quantity
                                    })
                                    .ToList()
                            })
                            .ToList()
            }).ToList();

            return result;
        }

        public async Task<ActionResult<UserGetDTO>> GetSingleUser(int id)
        {
            var query = await _context.Users
                        .Include(u => u.Carts)
                            .ThenInclude(o => o.CartProducts)
                            .ThenInclude(op => op.Product)
                        .Where(u => u.Id == id)
                        .FirstOrDefaultAsync();

            if (query == null)
            {
                return null;
            }
            var result = new UserGetDTO
            {
                Id = query.Id,
                UserName = query.UserName,
                UserEmail = query.UserEmail,
                Role = query.Role,
                Carts = query.Carts
                             .Select(order => new CartGetDTO
                             {
                                 Id = order.Id,
                                 UserId = order.UserId,
                                 TotalPrice = order.CartProducts
                                    .Where(orderProduct => orderProduct.Product != null)
                                    .Sum(orderProduct => orderProduct.Quantity * orderProduct.Product.Price),
                                 Products = order.CartProducts
                                    .Select(orderProduct => new ProductGetDTO
                                    {
                                        Id = orderProduct.Product != null ? orderProduct.Product.Id : 0,
                                        Title = orderProduct.Product != null ? orderProduct.Product.Title : string.Empty,
                                        Price = orderProduct.Product != null ? orderProduct.Product.Price : 0,
                                        Image = orderProduct.Product != null ? orderProduct.Product.Image : string.Empty,
                                        Quantity = orderProduct.Quantity
                                    })
                                    .ToList()
                             })
                            .ToList()
            };
            return result;
        }
        public async Task<OrderDto> GetOrderByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = o.UserId,
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
        public async Task<ActionResult<Product>> UpdateProduct(int id, ProductUpdateDTO request)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            product.Title = request.Title;
            product.Price = request.Price;
            product.Description = request.Description;
            product.Image = request.Image;
            product.UpdatedDate = DateTime.UtcNow;


            // Update the category if needed
            var categoryId = request.CategoryID;
            var existingCategory = await _context.Categories.FindAsync(categoryId);

            if (existingCategory == null)
            {
                return null;
            }

            product.CategoryID = categoryId;

            await _context.SaveChangesAsync();

            // Return the updated product
            return product;
        }

        public async Task<string> UpdateOrderById(OrderUpdateDto request)
        {

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == request.Id);
            if (order == null)
            {
                return "";
            }
            
            order.Status = request.Status;
            order.OrderUpdatedDate = DateTime.UtcNow;   
            await _context.SaveChangesAsync();
            return "Success";
        }
        public async Task<List<OrderDto>> GetOrdersCompletedAsync(OrderGetDto request)
        {
            var query = _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.IsActive == "N" && o.Status == "Completed");

            // Apply filtering based on OrderId, Status, and date range
            if (!string.IsNullOrEmpty(request.SearchText) && int.TryParse(request.SearchText, out int orderId))
            {
                query = query.Where(o => o.Id == orderId);
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(o => o.Status == request.Status);
            }

            if (request.OrderDateFrom != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate >= request.OrderDateFrom);
            }

            if (request.OrderDateEnd != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate <= request.OrderDateEnd);
            }

            // Apply sorting and pagination
            var orders = await query
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = o.UserId,
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
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return orders;
        }

        public async Task<List<OrderDto>> GetOrdersCanceledAsync(OrderGetDto request)
        {
            var query = _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.IsActive == "N" && o.Status == "Canceled");

            // Apply filtering based on OrderId, Status, and date range
            if (!string.IsNullOrEmpty(request.SearchText) && int.TryParse(request.SearchText, out int orderId))
            {
                query = query.Where(o => o.Id == orderId);
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(o => o.Status == request.Status);
            }

            if (request.OrderDateFrom != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate >= request.OrderDateFrom);
            }

            if (request.OrderDateEnd != DateTime.MinValue)
            {
                query = query.Where(o => o.OrderDate <= request.OrderDateEnd);
            }

            // Apply sorting and pagination
            var orders = await query
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    OrderUpdatedDate = o.OrderUpdatedDate,
                    UserId = o.UserId,
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
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return orders;
        }
    }
}
