using fakestrore_Net.DTOs.CategoryDTO;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.DTOs.UserDTO;
using Microsoft.AspNetCore.Mvc;

namespace fakestrore_Net.Services.AdminService
{
    public interface IAdminService
    {
        Task<ActionResult<List<Category>>> AddCategory(CategoryCreateDTO request);
        Task<ActionResult<List<Product>>> AddNewProduct(ProductCreateDTO request);
        Task<ActionResult<List<Product>>> DeleteProduct(int id);
        Task<ActionResult<Product>> UpdateProduct(int id, ProductUpdateDTO request);
        Task<ActionResult<List<UserGetDTO>>> GetAllUser();
        Task<ActionResult<UserGetDTO>> GetSingleUser(int id);
        Task<List<OrderDto>> GetAllOrderAsync();
        Task<OrderResultDto> GetOrderByStatus(OrderGetDto request);
        Task<OrderDto> GetOrderByOrderIdAsync(int orderId);
        Task<string>UpdateOrderById(OrderUpdateDto order);
        Task<List<OrderDto>> GetOrdersCompletedAsync(OrderGetDto request);
        Task<List<OrderDto>> GetOrdersCanceledAsync(OrderGetDto request);
    }
}
