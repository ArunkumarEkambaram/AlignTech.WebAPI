using AlignTech.WebAPI.Day1.Data;
using AlignTech.WebAPI.Day1.DTOs;
using AlignTech.WebAPI.Day1.Interfaces;
using AlignTech.WebAPI.Day1.Models;
using Microsoft.EntityFrameworkCore;

namespace AlignTech.WebAPI.Day1.Services
{
    public class ProductService : IProductService
    {
        private readonly MyStoreDbContext _dbContext;

        public ProductService(MyStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(bool? inStock)
        {
            var products = await _dbContext.Products.Include(p => p.Category).ToListAsync();

            if (inStock == true)
            {
                products = products.Where(x => x.StockAvailablility == true).ToList();
            }

            //Store product to ProductDto
            var productDto = products.Select(p => new ProductDto
            {
                ProductName = p.Name,
                CategoryName = p.Category.Name,
                Description = p.Description,
                IsInStock = p.StockAvailablility,
                Price = p.UnitPrice,
                Quantity = p.QuantityAvailable,
                AddedDate = p.AddedDate
            }).ToList();

            return productDto;
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            var productDto = new ProductDto
            {
                ProductName = product.Name ?? string.Empty,
                Description = product.Description,
                Price = product.UnitPrice,
                Quantity = product.QuantityAvailable,
                AddedDate = product.AddedDate,
                CategoryName = product.Category?.Name ?? string.Empty,
                IsInStock = product.StockAvailablility,
            };
            return productDto;
        }

        public async Task<Product> AddProduct(AddUpdateProductDto product)
        {
            //Convert DTO to Entity
            var newProduct = new Product
            {
                Name = product.ProductName ?? string.Empty,
                Description = product.Description,
                UnitPrice = product.Price,
                QuantityAvailable = product.Quantity,
                CategoryId = product.CategoryId
            };

            _dbContext.Products.Add(newProduct);//Temp Adding Product
            await _dbContext.SaveChangesAsync();//Apply to DB

            return newProduct;
        }

        public Task<ProductDto?> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }



        public Task<ProductDto?> UpdateProduct(int id, AddUpdateProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
