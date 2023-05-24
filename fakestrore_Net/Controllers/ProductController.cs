using fakestrore_Net.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace fakestrore_Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //Get all products in Category
        [HttpGet("Category/{name}")]
        public async Task<ActionResult<Category>> GetCategoryByName(string name)
        {
            try
            {
                var result = await _productService.GetCategoryByName(name);
                if (result == null)
                {
                    return NotFound("Not found Products");
                }

                var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Ok(json);
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }
    }
}
