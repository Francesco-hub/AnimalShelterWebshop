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
       private readonly IProductRepository _productRepo;
       private readonly IProductValidator _productValidator;


        public ProductService(IProductValidator productValidator, 
                                IProductRepository prodRepo)
        {
            if (productValidator == null) throw new NullReferenceException("Validator cannot be null");
            if (prodRepo == null) throw new NullReferenceException("ProductRepository cannot be null");
            
            _productRepo = prodRepo;
            _productValidator = productValidator;
        }
        
        public Product CreateProduct(Product prod)
        {
            //_productValidator.DefaultValidation(prod)
            return _productRepo.Create(prod);
        }
        
        public void DeleteProduct(int id)
        {
            _productRepo.Delete(id);
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

        public Product Update(Product product)
        {
            _productValidator.DefaultValidation(product);
            return null;
        }


    }
}
