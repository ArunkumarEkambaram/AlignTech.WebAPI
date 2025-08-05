using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AlignTech.WebAPI.DataFirst.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> AddProduct(AddProductDto addProductDto)
        {
            //Convert AddProductDto to Product
            Product product = new Product()
            {
                ProductId = addProductDto.ProductId,
                CategoryId = addProductDto.CategoryId,
                ProductName = addProductDto.ProductName,
                QuantityAvailable = addProductDto.QuantityAvailable,
                Price = addProductDto.Price,
            };
            var newProduct = await _repository.CreateProduct(product);
            //Convert Product to ProductDto
            var productDto = new ProductDto
            {
                Id = newProduct.ProductId,
                ProductName = newProduct.ProductName,
                CategoryName = newProduct.Category.CategoryName,
                Price = newProduct.Price,
                Quantity = newProduct.QuantityAvailable
            };

            return productDto;
        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return null!;
            }
            var productDto = new ProductDto
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                CategoryName = product.Category.CategoryName,
                Price = product.Price,
                Quantity = product.QuantityAvailable
            };

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            var productDto = products.Select(p => new ProductDto
            {
                ProductName = p.ProductName,
               // CategoryName = p.Category.CategoryName,
                Id = p.ProductId,
                Price = p.Price,
                Quantity = p.QuantityAvailable
            }).ToList();

            return productDto;
        }
    }
}
