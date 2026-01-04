using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repository.Data
{
    public class StoreContext:DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //   modelBuilder.ApplyConfiguration(new ProductConfigration())
            // modelBuilder.ApplyConfiguration(new ProductConfigration())
            // modelBuilder.ApplyConfiguration(new ProductTypeConfigration())
            //
            //
            // modelBuilder.ApplyConfiguration(  new OrderConfigration());
            //  modelBuilder.ApplyConfiguration(   new DeliveryMethod();
            //  modelBuilder.ApplyConfiguration (   new    OrderItem());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//any calss implement IEntityConfigration


        }        

        

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


    }
}
