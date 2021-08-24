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
    public class AutomatSlotProductConfiguration : IEntityTypeConfiguration<AutomatSlotProduct>
    {
        public void Configure(EntityTypeBuilder<AutomatSlotProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SlotId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
