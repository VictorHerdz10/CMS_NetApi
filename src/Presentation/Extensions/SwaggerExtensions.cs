using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CMS_NetApi.Presentation.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "CMS API", 
                Version = "v1",
                Description = "API para la gestion de registros de contratos",
                Contact = new OpenApiContact
                {
                    Name = "Soporte",
                    Email = "victorhernandezsalcedo4@gmail.com"
                }
            });
            
            // Configurar documentación XML
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            // Configuración de seguridad JWT
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingrese el token JWT en el formato: Bearer {token}"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // Configurar respuestas comunes
            c.OperationFilter<AddCommonResponseTypesOperationFilter>();
        });
        
        return services;
    }

    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => 
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMS API v1");
            c.DocumentTitle = "CMS API Documentation";
            c.DefaultModelExpandDepth(2);
            c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
            c.DisplayRequestDuration();
        });
        return app;
    }
}

// Filtro para agregar respuestas comunes
public class AddCommonResponseTypesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Respuestas comunes para todos los endpoints
        operation.Responses.TryAdd("400", new OpenApiResponse { 
            Description = "Solicitud incorrecta - Validación fallida" 
        });
        operation.Responses.TryAdd("401", new OpenApiResponse { 
            Description = "No autorizado" 
        });
        operation.Responses.TryAdd("500", new OpenApiResponse { 
            Description = "Error interno del servidor" 
        });
    }
}