using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlignTech.WebAPI.DataFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IValidator<AddProductDto> _validator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IValidator<AddProductDto> validator, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _validator = validator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            using var _ = _logger.BeginScope("Getting all products");
            // _logger.LogInformation("Getting all products");
            var products = await _productService.GetProducts();
            if (products == null)
            {
                _logger.LogWarning("There is nothing in the database to display");
                return NotFound(new { message = "Product is empty" });
            }
            _logger.LogInformation("Successfully retrieved all the products");
            return Ok(products);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetProduct/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(string id)
        {
            using var _ = _logger.BeginScope($"Retrieving Product Id :{id}");

            if (id == "P100")
            {
                _logger.LogError("Product Id : P100, cannot be used");
                throw new InvalidOperationException($"Product Id {id} is invalid");
            }
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                _logger.LogWarning($"No product found for Product Id :{id}");
                return NotFound(new { message = $"Product Id :{id} not found" });
            }
            _logger.LogInformation($"Successfully retrieved  Product Id :{id}");
            return Ok(product);
        }


        [Authorize(Roles = "seller, admin")]
        [HttpPost(Name = "AddProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductDto productDto)
        {
            using var _ = _logger.BeginScope($"Adding New product, Product Name :{productDto.ProductName}");

            var result = await _validator.ValidateAsync(productDto);
            if (!result.IsValid)
            {
                _logger.LogError("Validation Failed");
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
                _logger.LogWarning($"Successfully created a new product, Product Name :{productDto.ProductName}");
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            _logger.LogWarning($"Unable to add new product, Product Name :{productDto.ProductName}");
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

