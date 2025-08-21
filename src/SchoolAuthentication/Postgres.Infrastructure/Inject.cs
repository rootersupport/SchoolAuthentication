using Application.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Postgres.Infrastructure.Repositories;

namespace Postgres.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IAdministratorRepository, AdministratorRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IParentRepository, ParentRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
    }
}