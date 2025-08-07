using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using AutoMapper;

namespace AlignTech.WebAPI.DataFirst.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> AddProduct(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            var newProduct = await _repository.CreateProduct(product);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> GetProduct(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return null!;
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productDto;
        }

        public async Task<IEnumerable<ProductAndCategoryDto>> GetProductsAndCategories(short categoryId)
        {
            var productCategoryDto = await _repository.GetProductByCategory(categoryId);
            return productCategoryDto;
        }
    }
}
