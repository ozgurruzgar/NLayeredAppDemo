using Microsoft.EntityFrameworkCore;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.DataAccess.Concrete
{
    public class ProductDal
    {
        public List<Product> GetAll()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetById(int id)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Products.Where(p=>p.ProductId==id).SingleOrDefault();
            }
        }
        public void Add(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(product);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }
        public void Update(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(product);
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void Delete(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(product);
                addedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
