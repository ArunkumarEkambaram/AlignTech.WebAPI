using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AlignTech.WebAPI.DataFirst.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly QuickKartDbContext _dbContext;

        public ProductRepository(QuickKartDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _dbContext.Products.Include(p => p.Category).ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var product = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(x => x.ProductId == id);
            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            //Refresh the Category
            // var newProduct = await _dbContext.Products.Include(x => x.Category).FirstAsync(x => x.ProductId == product.ProductId);            
            await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
            return product;
        }

        public async Task<IEnumerable<ProductAndCategoryDto>> GetProductByCategory(short categoryId)
        {
            var result = await _dbContext.ProductAndCategoryDto.FromSqlInterpolated($"EXEC usp_GetProductCategory {categoryId}").ToListAsync();            
            return result;
        }
    }
}
