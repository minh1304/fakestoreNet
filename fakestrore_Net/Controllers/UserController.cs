using fakestrore_Net.DTOs.CartDTO;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
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

                /*  var json = JsonSerializer.Serialize(result, options);
  */
                // Tiếp tục xử lý JSON hoặc trả về JSON nếu cần thiết
                // ...

                return Ok(result);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }

    }
}
