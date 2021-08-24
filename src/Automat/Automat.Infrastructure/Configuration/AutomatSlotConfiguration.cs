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
    public class AutomatSlotConfiguration : IEntityTypeConfiguration<AutomatSlot>
    {
        public void Configure(EntityTypeBuilder<AutomatSlot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SlotNumber).IsRequired();
        }
    }
}
