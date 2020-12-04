using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.DomainService
{
    public interface IProductRepository
    {
        Product Create(Product prod);

        //Read data
        Product ReadProductByID(int id);

        IEnumerable<Product> ReadProductsByType(string type);
        IEnumerable<Product> ReadAllProducts(Filter filter = null);
        int Count();

        //Update data
        Product Update(Product prodUpdate);

        //Delete data
        Product Delete(int id);
    }
}

