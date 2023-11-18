using Microsoft.EntityFrameworkCore;
using supportDesk.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// config Dependency Injection

builder.Services.AddDbContext<SupportdeskdbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));


// Scaffold - DbContext "Server=GAUTAM\SQLEXPRESS; Database=IP_DBFirst; User Id=sa; Password=123456; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models

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
