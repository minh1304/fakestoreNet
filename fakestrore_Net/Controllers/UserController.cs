using fakestrore_Net.DTOs.CartDTO;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.Models;
using fakestrore_Net.Services.OrderService;
using fakestrore_Net.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace fakestrore_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer, Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public UserController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        [HttpPost("Cart")]
        public async Task<ActionResult<List<Product>>> AddCart(CartCreateDTO request)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var result = await _userService.AddCart(request);
                if (result == null)
                {
                    return BadRequest("Can't add product");
                }

                var json = JsonSerializer.Serialize(result, options);

                // Tiếp tục xử lý JSON hoặc trả về JSON nếu cần thiết
                // ...

                return Ok("Success!");
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }


        [HttpGet("Cart")]
        public async Task<ActionResult<List<Product>>> GetCart()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var result = await _userService.GetCart();
                if (result == null)
                {
                    return BadRequest("Can't found");
                }

                return Ok(result);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }

        [HttpPost("Order")]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(orderCreateDto);
                return Ok(result); // Return the success message
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Handle errors
            }
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersByUserIdAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        //Get Order Completed
        [HttpGet("getOrdersCompleted")]

        public async Task<IActionResult> GetOrdersCompleted()
        {
            var orders = await _orderService.GetOrdersCompletedAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        //Get Order Cancel 
        [HttpGet("getOrdersCanceled")]

        public async Task<IActionResult> GetOrdersCanceled()
        {
            var orders = await _orderService.GetOrdersCanceledAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpGet("Order/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByOrderIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // Complete Order 
        [HttpPost("Oder/Update/{orderId}")]
        public async Task<IActionResult> UpdateOrderById(int orderId)
        {
            var order = await _orderService.UpdateOrderById(orderId);
            if (order == false)
            {
                return NotFound();
            }
            return Ok("Success!");
        }

        // Cancel Oder 
        [HttpPost("Order/Cancel/{orderId}")]
        public async Task<IActionResult> CancelOrderById(int orderId)
        {
            var order = await _orderService.CancelOrderByIdAsync(orderId);
            if (order == false)
            {
                return NotFound();
            }
            return Ok("Success!");
        }

    }
}
