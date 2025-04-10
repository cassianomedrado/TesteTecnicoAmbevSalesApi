using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.DeveloperEvaluation.WebApi.SwaggerConfig;

/// <summary>
/// Classe para configurar documentação Swagger
/// </summary>
public class AuthResponsesOperationFilter : IOperationFilter
{
    /// <summary>
    /// Aplicar configuração para ícone de cadeado para endpoints com Authorize
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        if (authAttributes != null && authAttributes.Any())
        {
            var securityRequirement = new OpenApiSecurityRequirement()
        {
            {
                // Put here you own security scheme, this one is an example
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        };
            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        }
    }
}
