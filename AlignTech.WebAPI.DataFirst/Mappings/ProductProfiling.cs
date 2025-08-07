using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Models;
using AutoMapper;

namespace AlignTech.WebAPI.DataFirst.Mappings
{
    public class ProductProfiling : Profile
    {
        public ProductProfiling()
        {
            //Source: Product(Entity), Destination: ProductDto
            var get = CreateMap<Product, ProductDto>();
            get.ForMember(d => d.Id, option => option.MapFrom(s => s.ProductId));
            get.ForMember(d => d.Quantity, option => option.MapFrom(s => s.QuantityAvailable));

            //Source : ProductDto, Destination: Product
            var post = CreateMap<AddProductDto, Product>();
            post.ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId));
            post.ForMember(d=>d.ProductName, opt=>opt.MapFrom(s => s.ProductName));
            post.ForMember(d=>d.CategoryId, opt=>opt.MapFrom(s => s.CategoryId));
            post.ForMember(d=>d.Price, opt=>opt.MapFrom(s => s.Price));
            post.ForMember(d=>d.QuantityAvailable, opt=>opt.MapFrom(s => s.QuantityAvailable));
        }
    }
}
