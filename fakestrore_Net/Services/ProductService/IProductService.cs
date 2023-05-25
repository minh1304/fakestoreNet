using fakestore_Net.Filter;
using fakestrore_Net.Filter;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.ProductService
{
    public interface IProductService
    {
        Task<Category?> GetCategoryByName(string name);
        Task<List<object>> GetAllProducts([FromQuery] PaginationFilter filter, [FromQuery] SortFilter sortFilter);
        Task<object> GetProductById(int id);
    }
}
