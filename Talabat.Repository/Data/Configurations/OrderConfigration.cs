using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAdress, SA => SA.WithOwner());//1 to 1 total     address do not have table 
            builder.Property(O => O.Status)
                .HasConversion(


                OS => OS.ToString(),
                OS => (OrderStatus)Enum.Parse(typeof(OrderStatus), OS));

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");
           
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);// Solve cascade automatic delete delovery method

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
