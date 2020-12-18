using System;
using System.Collections.Generic;
using System.Text;
using WebshopApp.Core.Entity;

namespace WebshopApp.Core.ApplicationService
{
    public interface IProductValidator
    {
        public void DefaultValidation(Product product);

    }
}
