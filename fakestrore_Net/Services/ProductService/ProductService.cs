using fakestrore_Net.Data;
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

        public async Task<List<object>> GetAllProducts()
        {
            var productsWithCategoryName = await _context.Products
                .Include(p => p.Category) // Nạp thông tin danh mục vào kết quả truy vấn
                .Include(p => p.Rating)
                .Select(p => new
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name, // Lấy tên danh mục từ đối tượng Category
                    Rating = new
                    {
                        rate = p.Rating.Rate,
                        count = p.Rating.Count
                    }
                })
                .ToListAsync<object>();

            return productsWithCategoryName.Cast<object>().ToList();
        }

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
