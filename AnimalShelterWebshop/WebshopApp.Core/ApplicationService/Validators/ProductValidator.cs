using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService.Validators
{
    public class ProductValidator : IProductValidator
    {
        public void DefaultValidation(Product product)
        {
            if(product == null)
            {
                throw new NullReferenceException("Product cannot be null");
            }

            if(string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Product needs a name");
            }
        }
    }
}
