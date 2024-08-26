using fakestore_Net.Filter;
using fakestrore_Net.Data;
using fakestrore_Net.DTOs.CategoryDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.DTOs.RatingDTO;
using fakestrore_Net.Filter;
using fakestrore_Net.Models;
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

        //GET all products , pagination, sort 
        public async Task<List<ProductGetDTO>> GetAllProducts([FromQuery] PaginationFilter? filter, [FromQuery] SortFilter sortFilter)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Rating)
                .Where(p => p.IsActive == "Y")
                .Select(p => new ProductGetDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                    Image = p.Image,
                    Rating = new RatingGetDTO
                    {
                        Rate = p.Rating.Rate,
                        Count = p.Rating.Count,
                    }
                });

            //Nếu không truyền thì mặc định, giảm dần thì bật true, tăng dần thì false 
            if (sortFilter?.IsDescending == true)
            {
                productsQuery = productsQuery.OrderByDescending(p => p.Price);
            }
            else if (sortFilter?.IsDescending == false)
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }

            if (filter != null && filter.PageSize.HasValue && filter.PageNumber.HasValue)
            {
                int pageNumber = filter.PageNumber.Value < 1 ? 1 : filter.PageNumber.Value;
                int pageSize = filter.PageSize.Value > 10 ? 10 : filter.PageSize.Value;

                productsQuery = productsQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }

            var products = await productsQuery.ToListAsync();
            if (products == null)
            {
                return null;
            }
            return products;

        }


        //GET a single product
        public async Task<ProductGetDTO> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category) // Nạp thông tin danh mục vào kết quả truy vấn
                .Include(p => p.Rating)
                .Where(p => p.Id == id)
                .Select(p => new ProductGetDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                    CategoryID = p.Category.Id,
                    Image = p.Image,
                    Rating = new RatingGetDTO
                    {
                        Rate = p.Rating.Rate,
                        Count = p.Rating.Count
                    }
                })
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            return product;
        }
        //GET all Name category

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return categories;
        }

        //GET all products in Category
        public async Task<CategoryGetDTO?> GetCategoryByName(string name)
        {
            var product = await _context.Categories
                .Include(p => p.Products)
                .ThenInclude(p => p.Rating)
                .Where(p => p.Name == name)
                .Select(p => new CategoryGetDTO
                {
                    Name = p.Name,
                    Products = p.Products.Select(p => new ProductGetDTO
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Price = p.Price,
                        Description = p.Description,
                        Category = null,
                        Image = p.Image,
                        Rating = new RatingGetDTO
                        {
                            Rate = p.Rating.Rate,
                            Count = p.Rating.Count
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (product == null) { return null; }
            return product;


        }


    }
}