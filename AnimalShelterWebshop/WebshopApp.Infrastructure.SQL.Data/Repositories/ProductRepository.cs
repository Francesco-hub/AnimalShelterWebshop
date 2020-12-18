using Microsoft.EntityFrameworkCore;
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
      
        public void Delete(int id)
        {
            Product prod = _ctx.Products.FirstOrDefault(p => p.Id == id);
            _ctx.Entry(prod).Property("isDeleted").CurrentValue = true;
            _ctx.SaveChanges();
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            throw new NotImplementedException();
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
           
            Product prod = _ctx.Products.AsNoTracking().FirstOrDefault(p => p.Id == prodUpdate.Id);
            //prodUpdate.OrderProducts = prod.OrderProducts;
            //prodUpdate.Orders = prod.Orders;
            var entry = _ctx.Update(prodUpdate);
            _ctx.SaveChanges();
            return entry.Entity;
        }
    }
}
