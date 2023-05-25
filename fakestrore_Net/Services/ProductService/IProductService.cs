namespace fakestrore_Net.Services.ProductService
{
    public interface IProductService
    {
        Task<Category?> GetCategoryByName(string name);
        Task<List<object>> GetAllProducts();
    }
}
