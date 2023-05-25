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
        private string RemoveIdFromJson(string json)
        {
            // Phân tích chuỗi JSON thành đối tượng JObject
            JObject jObject = JObject.Parse(json);

            // Tìm tất cả các thuộc tính có tên "$id" và loại bỏ chúng
            foreach (JProperty property in jObject.Properties().ToList())
            {
                if (property.Name == "$id")
                {
                    property.Remove();
                }
            }

            // Chuyển đối tượng JObject thành chuỗi JSON đã chỉnh sửa
            string editedJson = jObject.ToString();

            return editedJson;
        }
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //Get all Products 
        [HttpGet()]
        public async Task<ActionResult<List<object>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                if (products == null)
                {
                    return NotFound("Not found Products");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving products: " + ex.Message);
                return BadRequest("Can't retrieve products");
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
