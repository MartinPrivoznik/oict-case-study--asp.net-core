using Asp.Versioning;
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

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        services.ConfigureOptions<SwaggerGenNamedOptions>();

        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}