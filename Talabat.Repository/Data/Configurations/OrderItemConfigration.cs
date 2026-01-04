using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.ItemOrdered, io =>
            {
                io.WithOwner();
            });


            builder.Property(O => O.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
