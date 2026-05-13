using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces;
using Trainova.Infrastructure.Persistence;
using Trainova.Infrastructure.Security;

namespace Trainova.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAppDbContext>(provider =>
            provider.GetRequiredService<AppDbContext>());

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
