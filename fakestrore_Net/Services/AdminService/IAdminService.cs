using fakestrore_Net.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.AdminService
{
    public interface IAdminService
    {
        Task<ActionResult<List<Category>>> AddCategory(CategoryCreateDTO request);

        Task<ActionResult<List<Product>>> AddNewProduct(ProductCreateDTO request);
        Task<ActionResult<List<Product>>> DeleteProduct(int id);
    }
}
