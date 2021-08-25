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
    public class PaymentTypeOptionConfiguration : IEntityTypeConfiguration<PaymentTypeOption>
    {
        public void Configure(EntityTypeBuilder<PaymentTypeOption> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.RefundPaymentStatus).IsRequired();
            builder.Property(x => x.CreatedDate).HasColumnType("datetime");
            builder.Property(x => x.ModifiedDate).HasColumnType("datetime");
        }
    }
}
