using AlignTech.WebAPI.Day1.DTOs;
using AlignTech.WebAPI.Day1.Models;

namespace AlignTech.WebAPI.Day1.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts(bool? inStock);
        Task<ProductDto?> GetProductById(int id);
        Task<Product> AddProduct(AddUpdateProductDto product);
        Task<ProductDto?> UpdateProduct(int id, AddUpdateProductDto product);
        Task<ProductDto?> DeleteProduct(int id);
    }
}
