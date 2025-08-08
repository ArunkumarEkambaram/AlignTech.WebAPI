using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AlignTech.WebAPI.DataFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<AddProductDto> _validator;

        public ProductsController(IProductService productService, IValidator<AddProductDto> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
             //throw new Exception("Base Exception");
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
            if (id == "P100")
            {
                throw new InvalidOperationException($"Product Id {id} is invalid");
            }
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
            var result = await _validator.ValidateAsync(productDto);
            if (!result.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation Failed",
                    errors = result.Errors.Select(x => new
                    {
                        property = x.PropertyName,
                        errorMsg = x.ErrorMessage
                    })
                });
            }

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

