using fakestrore_Net.Data;
using fakestrore_Net.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetAllProducts([FromQuery] PaginationFilter? filter)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Rating)
                .Select(p => new
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                    Image = p.Image,
                    Rating = new
                    {
                        rate = p.Rating.Rate,
                        count = p.Rating.Count
                    }
                });
            if (filter != null && filter.PageSize.HasValue && filter.PageNumber.HasValue)
            {
                int pageNumber = filter.PageNumber.Value < 1 ? 1 : filter.PageNumber.Value;
                int pageSize = filter.PageSize.Value > 10 ? 10 : filter.PageSize.Value;

                productsQuery = productsQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }
            var products = await productsQuery.ToListAsync<object>();
            return products;
        }


        //GET a single product
        public async Task<object> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category) // Nạp thông tin danh mục vào kết quả truy vấn
                .Include(p => p.Rating)
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name, // Lấy tên danh mục từ đối tượng Category
                    Image = p.Image,
                    Rating = new
                    {
                        rate = p.Rating.Rate,
                        count = p.Rating.Count
                    }
                })
                .FirstOrDefaultAsync();
            return product;
        }


        //GET all products in Category
        public async Task<Category?> GetCategoryByName(string name)
        {
            var category = await _context.Categories
                .Include(p => p.Products)
                .ThenInclude(r => r.Rating)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (category is null)
            {
                return null;
            }

            return category;
        }


    }
}
