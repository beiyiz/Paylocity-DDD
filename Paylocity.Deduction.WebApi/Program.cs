using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Paylocity.Deduction.Core.Aggregates;
using Paylocity.Deduction.Core.Aggregates.Rule;
using Paylocity.Deduction.Core.Interfaces;
using Paylocity.Deduction.Infrastructure.Data;
using Paylocity.Deduction.SharedKernel.Interface;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(c =>
          c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var assemblies = new Assembly[]
     {
        typeof(AppDbContext).Assembly,
        typeof(Employee).Assembly
     };
 builder.Services.AddMediatR(assemblies);

builder.Services.AddScoped<IRepository<Employee>, EfRepository<Employee> > ();
builder.Services.AddSingleton<IDecuctionRule, DeductionRule>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
