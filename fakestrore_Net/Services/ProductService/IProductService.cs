using fakestore_Net.Filter;
using fakestrore_Net.DTOs.CategoryDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.Filter;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.ProductService
{
    public interface IProductService
    {
        Task<List<string>> GetAllCategories();
        Task<CategoryGetDTO?> GetCategoryByName(string name);
        Task<List<ProductGetDTO>> GetAllProducts([FromQuery] PaginationFilter filter, [FromQuery] SortFilter sortFilter);
        Task<ProductGetDTO> GetProductById(int id);
    }
}
