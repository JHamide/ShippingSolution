using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.Entities;
using Shipping.Core.ValueObjects;
using Shipping.Infrastructure.Persistence;
using Xunit;

public class OrderTests
{
    [Fact]
    public void Order_WithLines_TotalIsCorrect()
    {
        // 1. ایجاد DbContext در حافظه
        var options = new DbContextOptionsBuilder<ShippingDbContext>()
            .UseSqlite("Data Source=:memory:") // SQLite in-memory
            .Options;

        using var context = new ShippingDbContext(options);
        context.Database.OpenConnection();  // برای SQLite in-memory
        context.Database.EnsureCreated();

        // 2. ایجاد یک Order
        var order = new Order(customerId: Guid.NewGuid());
        order.AddLine("SKU1", 2, new Money(100, "USD"));
        order.AddLine("SKU2", 3, new Money(50, "USD"));

        // 3. ذخیره در دیتابیس
        context.Orders.Add(order);
        context.SaveChanges();

        // 4. خواندن دوباره از دیتابیس
        var savedOrder = context.Orders
            .Include(o => o.Lines)
            .FirstOrDefault(o => o.Id == order.Id);

        savedOrder.Should().NotBeNull();
        savedOrder.Lines.Count.Should().Be(2);

        // 5. محاسبه Total
        var total = savedOrder.GetTotal();
        total.Amount.Should().Be(2 * 100 + 3 * 50); // 350
        total.Currency.Should().Be("USD");
    }
}
