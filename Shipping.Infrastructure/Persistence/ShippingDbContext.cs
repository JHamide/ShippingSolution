using Microsoft.EntityFrameworkCore;
using Shipping.Core.Entities;

namespace Shipping.Infrastructure.Persistence
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders");
                builder.HasKey(o => o.Id);

                builder.Property(o => o.CustomerId)
                       .IsRequired();

                builder.HasMany(o => o.Lines)
                       .WithOne()
                       .HasForeignKey("OrderId")
                       .OnDelete(DeleteBehavior.Cascade);

                builder.Navigation(o => o.Lines)
                       .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // Mapping OrderLine
            modelBuilder.Entity<OrderLine>(line =>
            {
                line.ToTable("OrderLines");
                line.HasKey(l => l.Id);

                line.Property(l => l.SKU)
                       .IsRequired()
                       .HasMaxLength(50);

                line.Property(l => l.Quantity)
                       .IsRequired();

                line.OwnsOne(l => l.UnitPrice, money =>
                {
                    money.Property(m => m.Amount).HasColumnName("UnitPriceAmount");
                    money.Property(m => m.Currency).HasColumnName("UnitPriceCurrency").HasMaxLength(3);
                });
            });
        }
    }
}
