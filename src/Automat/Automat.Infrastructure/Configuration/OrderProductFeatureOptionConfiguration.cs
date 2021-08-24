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
    public class OrderProductFeatureOptionConfiguration : IEntityTypeConfiguration<OrderProductFeatureOption>
    {
        public void Configure(EntityTypeBuilder<OrderProductFeatureOption> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderDetailId).IsRequired();
            builder.Property(x => x.FeatureOptionId).IsRequired();
            builder.Property(x => x.Quantity);
        }
    }
}
