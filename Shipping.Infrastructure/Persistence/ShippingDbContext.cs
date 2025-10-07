using Microsoft.EntityFrameworkCore;
using Shipping.Core.Entities;
using Shipping.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Persistence
{
    public class ShippingDbContext : DbContext
    {
        public ShippingDbContext(DbContextOptions<ShippingDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Order table
            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(o => o.Id);

                // map private backing field _lines as an owned collection
                order.OwnsMany(typeof(OrderLine), "_lines", ol =>
                {
                    ol.WithOwner().HasForeignKey("OrderId");
                    ol.Property<Guid>("Id");
                    ol.HasKey("Id");

                    ol.Property<string>("SKU").HasColumnName("SKU").IsRequired();
                    ol.Property<int>("Quantity").IsRequired();

                    // map Money as owned type inside OrderLine
                    ol.OwnsOne(typeof(Money), "Price", mb =>
                    {
                        mb.Property<decimal>("Amount").HasColumnName("PriceAmount");
                        mb.Property<string>("Currency").HasColumnName("PriceCurrency");
                    });

                    ol.ToTable("OrderLines"); // put owned collection in separate table
                });

                order.ToTable("Orders");
            });
        }
    }
}
