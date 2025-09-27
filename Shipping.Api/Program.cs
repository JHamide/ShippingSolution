using Shipping.Application.DTOs;
using Shipping.Application.UseCases;
using Shipping.Core.Interfaces;
using Shipping.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IOrderRepository,InMemoryOrderRepository>();
builder.Services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/orders", async (CreateOrderRequest request, ICreateOrderUseCase uc) =>
{
    var id = await uc.ExecuteAsync(request);
    return Results.Created($"/orders/{id}", new { id });
});

app.MapGet("/orders/{id:guid}", async (Guid id, IOrderRepository repo) =>
{
    var order = await repo.GetByIdAsync(id);
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.Run();
