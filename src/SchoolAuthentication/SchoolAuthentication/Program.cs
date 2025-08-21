using System.Reflection;
using System.Text;
using Application;
using Application.Configuration;
using Application.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Postgres.Infrastructure;
using Postgres.Infrastructure.Data;
using Presentation.Shared.ExceptionsFilters;

var builder = WebApplication.CreateBuilder(args);

// --- Добавляем DbContext ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
//JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// --- Добавляем Application и Infrastructure слои ---
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

// Авторизация и аутентификация
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();

// --- Swagger и версионирование ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SchoolAuthentication",
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseAuthorization();

// --- Подключаем контроллеры ---
app.MapControllers();

app.Run();
