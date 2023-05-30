using fakestrore_Net.Data;
using fakestrore_Net.DTOs.CategoryDTO;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.DTOs.UserDTO;
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
                Name = request.Name
            };
            var products = request.Products.Select(p => new Product
            {
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                Image = p.Image,
                Rating = new Rating
                {
                    Rate = p.Rating.Rate,
                    Count = p.Rating.Count
                },
                Category = newCategory
            }).ToList();
            newCategory.Products = products;
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
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<ActionResult<List<UserGetDTO>>> GetAllUser()
        {
            var query = await _context.Users
                        .Include(u => u.Orders)
                            .ThenInclude(o => o.OrderProducts)
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
                PasswordHash = user.PasswordHash,
                Orders = user.Orders

                            .Select(order => new OrderGetDTO
                            {
                                UserId = order.Id,
                                TotalPrice = order.OrderProducts
                                    .Where(orderProduct => orderProduct.Product != null)
                                    .Sum(orderProduct => orderProduct.Quantity * orderProduct.Product.Price),
                                Products = order.OrderProducts
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
                        .Include(u => u.Orders)
                            .ThenInclude(o => o.OrderProducts)
                            .ThenInclude(op => op.Product)
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
                PasswordHash = query.PasswordHash,
                Orders = query.Orders
                             .Select(order => new OrderGetDTO
                             {
                                 UserId = order.Id,
                                 TotalPrice = order.OrderProducts
                                    .Where(orderProduct => orderProduct.Product != null)
                                    .Sum(orderProduct => orderProduct.Quantity * orderProduct.Product.Price),
                                 Products = order.OrderProducts
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

    }
}
