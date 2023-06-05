using fakestore_Net.Filter;
using fakestrore_Net.DTOs.ProductDTO;
using fakestrore_Net.Filter;
using fakestrore_Net.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [HttpGet]
        public async Task<ActionResult<List<ProductGetDTO>>> GetAllProducts([FromQuery] PaginationFilter filter, [FromQuery] SortFilter sortFilter)
        {
            try
            {
                var result = await _productService.GetAllProducts(filter, sortFilter);
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

                return Ok(json);
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("JSON serialization error: " + ex.Message);
                return BadRequest("Unable to serialize the result.");
            }
        }
        //GET a single product
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProductById(int id)
        {
            try
            {
                var result = await _productService.GetProductById(id);
                if (result == null)
                {
                    return NotFound("Not found Products");
                }

                var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                //Chỉnh cho đẹp
                JObject jObject = JObject.Parse(json);

                json = jObject.ToString();

                return Ok(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }
        //Get all name category
        [HttpGet("Categories")]
        public async Task<ActionResult<List<string>>> GetAllCategories()
        {
            try
            {
                var result = await _productService.GetAllCategories();
                if (result == null)
                {
                    return BadRequest("Don't Found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
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
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
                //Chỉnh cho đẹp
                JObject jObject = JObject.Parse(json);
                json = jObject.ToString();

                return Ok(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi serialize JSON: " + ex.Message);
                return BadRequest("Can't serialize result");
            }
        }
    }
}
