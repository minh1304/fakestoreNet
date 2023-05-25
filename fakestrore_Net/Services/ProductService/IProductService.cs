using fakestrore_Net.Filter;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.ProductService
{
    public interface IProductService
    {
        Task<Category?> GetCategoryByName(string name);
        Task<List<object>> GetAllProducts([FromQuery] PaginationFilter filter);
        Task<object> GetProductById(int id);
    }
}
