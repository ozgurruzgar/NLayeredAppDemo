using Northwind.DataAccess.Concrete;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business.Concrete
{
    public class ProductManager
    {
        ProductDal productDal = new ProductDal();
        public List<Product> GetAll()
        {
            //Business Codes

            return productDal.GetAll();
        }
    }
}
