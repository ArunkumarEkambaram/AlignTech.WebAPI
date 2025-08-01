using AlignTech.WebAPI.Day1.Interfaces;
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
        public IActionResult Get([FromQuery] bool? inStock)
        {
            var products = _productService.GetProducts(inStock);
            return Ok(products);
        }

        [HttpGet("GetId/{id?}")]
        //GET : localhost:123/api/Products/GetId/
        public IActionResult GetProduct(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id cannot be null");
            }
            var product = _productService.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
