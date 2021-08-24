using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Automat.Infrastructure.Context
{
    public class AutomatContext : DbContext
    {
        public AutomatContext(DbContextOptions<AutomatContext> options) : base(options)
        {
        }

        public DbSet<AutomatSlot> AutomatSlots { get; set; }
        public DbSet<AutomatSlotProduct> AutomatSlotProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderProductFeatureOption> OrderProductFeatureOptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryFeature> CategoryFeatures { get; set; }
        public DbSet<CategoryFeatureOption> CategoryFeatureOptions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentTypeOption> PaymentTypeOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(builder);
        }
    }
}
