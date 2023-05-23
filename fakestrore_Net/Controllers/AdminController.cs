using fakestrore_Net.Data;
using fakestrore_Net.DTOs;
using fakestrore_Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
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
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCharacterById(int id)
        {
            var category = await _context.Categories
               .Include(p => p.Products)
               .ThenInclude(r => r.Rating)
               .FirstOrDefaultAsync(c => c.Id == id);
            return Ok(category);
        }
    }
}

/*            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return await _context.Categories.ToListAsync();*/
