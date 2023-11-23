using Microsoft.EntityFrameworkCore;
using supportDesk.Helper;
using supportDesk.Models;
using supportDesk.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// config Dependency Injection
builder.Services.AddDbContext<SupportdeskdbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));

// Injection for Email config from appsetting.json
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettions"));

// add services

builder.Services.AddTransient<IEmailService, EmailService>();


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
