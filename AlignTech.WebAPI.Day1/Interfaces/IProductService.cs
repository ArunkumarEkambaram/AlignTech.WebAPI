using AlignTech.WebAPI.Day1.Models;

namespace AlignTech.WebAPI.Day1.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(bool? inStock);
        Product? GetProductById(int id);
        Product AddProduct(Product product);
        Product? UpdateProduct(int id, Product product);
        Product? DeleteProduct(int id);
    }
}
