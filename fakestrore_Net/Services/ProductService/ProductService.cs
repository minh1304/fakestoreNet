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
