using BusinessObject.Models;
using DataAccess.DAOS;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Business;
using Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
builder.Services.AddScoped<IproductDao, ProductDao>();
builder.Services.AddDbContext<Prn221DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("prn_db")));
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
