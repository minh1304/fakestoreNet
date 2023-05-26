using fakestrore_Net.DTOs;
using fakestrore_Net.Services.AdminService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace fakestrore_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}

