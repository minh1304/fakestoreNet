﻿using fakestrore_Net.DTOs.CategoryDTO;
using fakestrore_Net.DTOs.OrderDTO;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.DTOs.UserDTO;
using fakestrore_Net.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace fakestrore_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        //Add category
        [HttpPost("categories")]
        public async Task<ActionResult<List<Category>>> AddCategory(CategoryCreateDTO request)
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
                var result = await _adminService.AddCategory(request);
                if (result == null)
                {
                    return BadRequest("Can't add category");
                }

                var json = System.Text.Json.JsonSerializer.Serialize(result, options);

                // Tiếp tục xử lý JSON hoặc trả về JSON nếu cần thiết
                // ...

                return Ok("Success!");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }


        //Add Product 
        //POST product
        [HttpPost("product")]
        public async Task<ActionResult<List<Product>>> AddNewProduct(ProductCreateDTO request)
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
                var result = await _adminService.AddNewProduct(request);
                if (result == null)
                {
                    return BadRequest("Can't add product");
                }

                var json = System.Text.Json.JsonSerializer.Serialize(result, options);

                // Tiếp tục xử lý JSON hoặc trả về JSON nếu cần thiết
                // ...

                return Ok("Success!");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }

        //Remove by id
        [HttpPatch("deleteProduct/{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
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
                var result = await _adminService.DeleteProduct(id);
                if (result == null)
                {
                    return BadRequest("Can't remove product");
                }

                var json = System.Text.Json.JsonSerializer.Serialize(result, options);

                // Tiếp tục xử lý JSON hoặc trả về JSON nếu cần thiết
                // ...

                return Ok("Success!");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }

        //Update product
        [HttpPut("product/{id}")]
        public async Task<ActionResult<List<Product>>> UpdateProduct(int id, ProductUpdateDTO request)
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
                var result = await _adminService.UpdateProduct(id, request);
                if (result == null)
                {
                    return BadRequest("Product not found");
                }
                var json = System.Text.Json.JsonSerializer.Serialize(result, options);
                return Ok("Success!");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }

        //admin ms làm
        [HttpGet("user")]
        public async Task<ActionResult<List<UserGetDTO>>> GetAllUser()
        {
            try
            {
                var result = await _adminService.GetAllUser();
                if (result == null)
                {
                    return BadRequest("Unable to retrieve orders.");
                }

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(result, Formatting.None, settings);

                JObject jObject = JObject.Parse(json);
                json = jObject.ToString();

                return Ok(json);
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("JSON serialization error: " + ex.Message);
                return BadRequest("Unable to serialize the result.");
            }
        }
        //admin ms làm
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserGetDTO>> GetSigleUSer(int id)
        {
            try
            {
                var result = await _adminService.GetSingleUser(id);
                if (result == null)
                {
                    return BadRequest("Unable to retrieve orders.");
                }

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(result, Formatting.None, settings);

                JObject jObject = JObject.Parse(json);
                json = jObject.ToString();

                return Ok(json);
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("JSON serialization error: " + ex.Message);
                return BadRequest("Unable to serialize the result.");
            }
        }
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrder()
        {
            var orders = await _adminService.GetAllOrderAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);

        }
        [HttpPost("getOrderByStatus")]
        public async Task<IActionResult> GetOrderByStatus([FromBody] OrderGetDto request)
        {
            var orders = await _adminService.GetOrderByStatus(request);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }
        // GET Oder by ID 
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _adminService.GetOrderByOrderIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        //Get Order Completed
        [HttpGet("getOrdersCompleted")]

        public async Task<IActionResult> GetOrdersCompleted([FromBody] OrderGetDto request)
        {
            var orders = await _adminService.GetOrdersCompletedAsync(request);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        //Get Order Cancel 
        [HttpGet("getOrdersCanceled")]
        public async Task<IActionResult> GetOrdersCanceled([FromBody] OrderGetDto request)
        {
            var orders = await _adminService.GetOrdersCanceledAsync(request);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }
        [HttpPatch("UpdateOrderById")]
        public async Task<IActionResult> UpdateOrderById([FromBody] OrderUpdateDto orderCreateDto)
        {
            var result = await _adminService.UpdateOrderById(orderCreateDto);
            if (!String.IsNullOrEmpty(result)) {
                return Ok("Success!");
            }
            else { return BadRequest(); }
        }
    }
}

