using AlignTech.WebAPI.Day1.DTOs;
using AlignTech.WebAPI.Day1.Interfaces;
using AlignTech.WebAPI.Day1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlignTech.WebAPI.Day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool? inStock)
        {
            var products = await _productService.GetProducts(inStock);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        //GET : localhost:123/api/Products/GetProduct/{id}
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] AddUpdateProductDto product)
        {
            var newProduct = await _productService.AddProduct(product);
            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);//RouteName, RouteValue, Model
        }

        //[HttpPut("UpdateProduct/{id}")]
        //public IActionResult Put(int id, [FromBody] Product product)
        //{

        //}
    }
}
