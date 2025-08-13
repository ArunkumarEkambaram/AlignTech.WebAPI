using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AlignTech.WebAPI.DataFirst.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly QuickKartDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        //Const
        private const string ALL_PRODUCTS = "allProducts";
        //private const string PRODUCT_BY_ID = "2";

        public ProductRepository(QuickKartDbContext dbContext, ILogger<ProductRepository> logger, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _logger = logger;
            _cache = cache;

            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(1),
                //AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                SlidingExpiration = null,
                Priority = CacheItemPriority.Normal
            };
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //Try to get the value from Cache
            if (_cache.TryGetValue(ALL_PRODUCTS, out IEnumerable<Product>? cachedProduct))
            {
                _logger.LogInformation("Retrived all the products from Cache");
                return cachedProduct!;
            }

            //Get Value from Database
            _logger.LogInformation("Cache miss - Retreiving all the products from database");
            var products = await _dbContext.Products.Include(p => p.Category).ToListAsync();

            //Store Value to Cache
            _cache.Set(ALL_PRODUCTS, products, _cacheOptions);
            _logger.LogInformation($"Cached {products.Count()} products");
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

            //Invalidate Cache
            if (_cache.Get(ALL_PRODUCTS) != null)
            {
                _cache.Remove(ALL_PRODUCTS);
            }

            //Refresh the Category
            // var newProduct = await _dbContext.Products.Include(x => x.Category).FirstAsync(x => x.ProductId == product.ProductId);            
            await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
            return product;
        }

        public async Task<IEnumerable<ProductAndCategoryDto>> GetProductByCategory(short categoryId)
        {
            var catId = new SqlParameter("@CategoryId", System.Data.SqlDbType.TinyInt);
            catId.Value = categoryId;
            var result = _dbContext.ProductAndCategoryDto.FromSqlRaw("EXEC usp_GetProductCategory", catId);
            //var result = await _dbContext.ProductAndCategoryDto.FromSqlInterpolated($"EXEC usp_GetProductCategory {categoryId}").ToListAsync();            
            return result;
        }
    }
}
