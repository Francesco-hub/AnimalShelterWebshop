using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebshopApp.Core.DomainService;
using WebshopApp.Core.Entity;

namespace WebshopApp.Infrastructure.SQL.Data.Repositories
{
    public class ProductRepository : IProductRepository
    { 
        readonly WebshopAppContext _ctx;

    public ProductRepository(WebshopAppContext ctx)
    {
        _ctx = ctx;
    }
    
        public int Count()
        {
            return _ctx.Products.Count();
        }

        public Product Create(Product prod)
        {
            var product = _ctx.Products.Add(prod).Entity;
            _ctx.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {
            var prodRemoved = _ctx.Remove<Product>(new Product { Id = id }).Entity;
            _ctx.SaveChanges();
            return prodRemoved;
        }

        public IEnumerable<Product> ReadAllProducts(Filter filter = null)
        {
            return _ctx.Products;
        }

        public Product ReadProductByID(int id)
        {
            var changeTracker = _ctx.ChangeTracker.Entries<Product>();
            return _ctx.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> ReadProductsByType(string type)
        {
            List<Product> prodLst = _ctx.Products.Where(p => p.TypeName.ToLower() == type.ToLower()).ToList();
            return prodLst;
        }

        public Product Update(Product prodUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
