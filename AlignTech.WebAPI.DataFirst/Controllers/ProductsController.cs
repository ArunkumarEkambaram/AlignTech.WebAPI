using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlignTech.WebAPI.DataFirst.Controllers
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
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound(new { message = "Product is empty" });
            }
            return Ok(products);
        }

        // [HttpGet("{id}", Name = "GetProduct")]
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound(new { message = $"Product Id :{id} not found" });
            }
            return Ok(product);
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto productDto)
        {
            var product = await _productService.AddProduct(productDto);
            if (product != null)
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
                // return Ok(product);
            }
            return NotFound();
        }

        [HttpGet("{categoryId}", Name = "GetProductByCategory")]
        public async Task<IActionResult> GetProductByCategory(short categoryId)
        {
            var productCategory = await _productService.GetProductsAndCategories(categoryId);
            if (productCategory.Any())
            {
                return Ok(productCategory);
            }
            return NotFound(new { message = $"Category Id {categoryId} not found." });
        }
    }
}

