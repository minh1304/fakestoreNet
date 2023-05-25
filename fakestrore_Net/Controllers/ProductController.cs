﻿using fakestrore_Net.Filter;
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
        public async Task<ActionResult<List<object>>> GetAllProducts([FromQuery] PaginationFilter filter)
        {
            try
            {
                var products = await _productService.GetAllProducts(filter);
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
