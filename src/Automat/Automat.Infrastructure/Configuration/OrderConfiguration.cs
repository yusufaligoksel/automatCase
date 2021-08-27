using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Automat.Infrastructure.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProcessId).IsRequired();
            builder.Property(x => x.OrderCode).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired();
            builder.Property(x => x.AutomatSlotId).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PaymentTypeOptionId).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired().HasMaxLength(800);
            builder.Property(x => x.PaymentTotal).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.RefundAmount).HasColumnType("decimal(18,2)");
            builder.Property(x => x.OrderDate).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.CreatedDate).HasColumnType("datetime");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
        }
    }
}
