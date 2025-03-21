using Microsoft.EntityFrameworkCore;
using NTTDataSales.Domain.Context;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("pgsql");

// Add services to the container.
builder.Services.AddDbContext<NttContext>(options =>
    options.UseNpgsql(connStr, opts => opts.MigrationsAssembly("NTTDataSales.Domain")));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
