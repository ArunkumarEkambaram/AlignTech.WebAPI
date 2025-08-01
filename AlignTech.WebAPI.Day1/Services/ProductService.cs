﻿using AlignTech.WebAPI.Day1.Interfaces;
using AlignTech.WebAPI.Day1.Models;

namespace AlignTech.WebAPI.Day1.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product{ Id = 1, Name="Mouse", Description= "Logitech Wireless Mouse", UnitPrice=5500, QuantityAvailable=50, AddedDate= new DateTime()},
                new Product{ Id = 2, Name="WebCam", Description= "Lenovo Webcam", UnitPrice=2800, QuantityAvailable=150, AddedDate= new DateTime()},
                new Product{ Id = 3, Name="Speaker", Description= "Logitech Speaker", UnitPrice=1800, QuantityAvailable=0, AddedDate= new DateTime()},
            };
        }

        public IEnumerable<Product> GetProducts(bool? inStock)
        {
            var result = inStock == true ? _products.Where(p => p.StockAvailablility == inStock) : _products;
            return result;
        }

        public Product? GetProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public Product AddProduct(Product product)
        {
            var id = _products.Max(x => x.Id) + 1;
            var newProduct = product;
            newProduct.Id = id;
            _products.Add(newProduct);
            return newProduct;
        }

        public Product? DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }


        public Product? UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
