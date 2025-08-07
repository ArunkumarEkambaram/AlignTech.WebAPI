using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Models;

namespace AlignTech.WebAPI.DataFirst.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(string id);

        Task<Product> CreateProduct(Product product);

        Task<IEnumerable<ProductAndCategoryDto>> GetProductByCategory(short categoryId);
    }
}
