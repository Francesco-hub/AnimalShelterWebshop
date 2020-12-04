using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService.Services
{
    public class ProductService : IProductService
    {
        readonly IProductRepository _productRepo;

        public ProductService(IProductRepository prodRepo)
        {
            _productRepo = prodRepo;
        }
        public Product CreateProduct(Product prod)
        {
            return _productRepo.Create(prod);
        }

        public Product DeleteProduct(int id)
        {
            return _productRepo.Delete(id);
        }

        public Product FindProductByID(int id)
        {
            return _productRepo.ReadProductByID(id);
        }

        public List<Product> GetAllByType(string type)
        {
            return _productRepo.ReadProductsByType(type).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _productRepo.ReadAllProducts().ToList();
        }

        public Product UpdateProduct(Product ProductUpdate)
        {
            return _productRepo.Update(ProductUpdate);
        }
    }
}
