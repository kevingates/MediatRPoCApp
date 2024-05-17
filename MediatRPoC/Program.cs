using MediatRPoC.Data;
using MediatRPoC.Entities;
using MediatRPoC.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseInMemoryDatabase("TestDb"));


// Register Unit of Work and Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(provider.GetService<AppDbContext>()));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Customer>());

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

app.Run();
