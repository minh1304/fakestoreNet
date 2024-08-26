using fakestrore_Net.DTOs.OrderDTO;

namespace fakestrore_Net.Services.OrderService
{
    public interface IOrderService
    {
        Task<string> CreateOrderAsync (OrderCreateDto order);
        Task<List<OrderDto>> GetOrdersByUserIdAsync();
        Task<List<OrderDto>> GetOrdersCompletedAsync();
        Task<List<OrderDto>> GetOrdersCanceledAsync();
        Task<OrderDto> GetOrderByOrderIdAsync(int orderId);
        Task<bool> CancelOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderById(int orderId);

    }
}
