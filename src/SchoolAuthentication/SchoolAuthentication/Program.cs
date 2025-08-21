using System.Reflection;
using Application;
using Application.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Postgres.Infrastructure;
using Postgres.Infrastructure.Data;
using Presentation.Shared.ExceptionsFilters;

var builder = WebApplication.CreateBuilder(args);

// --- Добавляем DbContext ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// --- Добавляем Application и Infrastructure слои ---
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// --- Swagger и версионирование ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SchoolCoreApi",
        Version = "v1"
    });
});

// --- Контроллеры с глобальным фильтром исключений ---
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

// --- Настройка версионирования API ---
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// --- CORS ---
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// --- Мидлвары ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolCoreApi v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

// --- Подключаем контроллеры ---
app.MapControllers();

app.Run();
