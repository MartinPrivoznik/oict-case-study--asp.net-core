using System.Reflection;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using OICTCaseStudy.Api.Authentication;
using OICTCaseStudy.Api.Configuration;
using OICTCaseStudy.Api.OpenApi;

namespace OICTCaseStudy.Api.Extensions;

public static class RestApiServiceCollectionExtensions
{
    public static IServiceCollection AddRestApiServices(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.Configure<ApiOptions>(configuration.GetSection(ApiOptions.SectionName));

        services.AddHealthChecks();
        services.AddControllers();

        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader());
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddAuthentication(ApiKeyAuthenticationHandler.SchemeName)
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
                ApiKeyAuthenticationHandler.SchemeName, _ => { });
        services.AddAuthorization();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(opt =>
        {
            opt.IncludeXmlComments(Assembly.GetExecutingAssembly());
            opt.AddSecurityDefinition(ApiKeyAuthenticationHandler.SchemeName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-Api-Key",
                Description = "API key passed in the X-Api-Key header"
            });
            opt.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(ApiKeyAuthenticationHandler.SchemeName, doc)] = []
            });
        });
        services.ConfigureOptions<SwaggerGenNamedOptions>();

        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}