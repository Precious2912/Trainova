using Microsoft.Extensions.DependencyInjection;

namespace Trainova.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register Application services
        return services;
    }
}
