using Microsoft.EntityFrameworkCore;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DataAccess.Concrete
{
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-1NLBACI\SQLSERVER;Database=Northwind;Trusted_Connection=true");
        }
        public DbSet<Product> Products { get; set; }
    }
}
