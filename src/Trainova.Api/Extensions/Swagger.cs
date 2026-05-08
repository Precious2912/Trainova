using Microsoft.OpenApi;

namespace Trainova.Api.Extensions;

public static class Swagger
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Trainova API",
                Version = "v1",
                Description = "Train journey search and booking API"
            });

            // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            // {
            //     Description = "JWT Authorization header. Example: 'Bearer {token}'",
            //     Name = "Authorization",
            //     In = ParameterLocation.Header,
            //     Type = SecuritySchemeType.ApiKey,
            //     Scheme = "Bearer"
            // });

            // c.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "Bearer"
            //             }
            //         },
            //         []
            //     }
            // });
        });

        return services;
    }
}
