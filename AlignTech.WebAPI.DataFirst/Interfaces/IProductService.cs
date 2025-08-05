using AlignTech.WebAPI.DataFirst.DTOs;

namespace AlignTech.WebAPI.DataFirst.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> GetProduct(string id);

        Task<ProductDto> AddProduct(AddProductDto addProductDto);
    }
}
