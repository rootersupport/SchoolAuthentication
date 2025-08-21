using Application.Contracts.Contracts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Inject
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAdministratorService, AdministratorService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IParentService, ParentService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}