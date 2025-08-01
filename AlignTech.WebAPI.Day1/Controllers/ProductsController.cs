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

        [HttpPost("AddProduct")]
        public IActionResult Post([FromBody]Product product)
        {
            var result =_productService.AddProduct(product);
            if (result.Id != 0)
            {
               // return CreatedAtRoute("AddProduct", product);
               return Ok(result);
            }
            return BadRequest();
        }

        //[HttpPut("UpdateProduct/{id}")]
        //public IActionResult Put(int id, [FromBody] Product product)
        //{

        //}
    }
}
