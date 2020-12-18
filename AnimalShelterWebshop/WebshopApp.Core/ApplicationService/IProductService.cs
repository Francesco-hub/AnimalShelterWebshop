using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService
{
    public interface IProductService
    {
        //Create - POST
       Product CreateProduct(Product prod);

        //Read - GET
        Product FindProductByID(int id);
        List<Product> GetAllProducts();
        List<Product> GetAllByType(string type);

        //Update - PUT
        Product UpdateProduct(Product ProductUpdate);

        //Delete - DELETE
        void DeleteProduct(int id);
        Product Update(Product product);
    }
}
