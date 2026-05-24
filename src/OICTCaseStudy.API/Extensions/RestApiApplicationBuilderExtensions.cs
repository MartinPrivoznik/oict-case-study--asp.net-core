using System.Globalization;
using OICTCaseStudy.Api.Configuration;
using OICTCaseStudy.Api.Middleware;

namespace OICTCaseStudy.Api.Extensions;

public static class RestApiApplicationBuilderExtensions
{
    public static WebApplication UseRestApi(this WebApplication app)
    {
        var apiOptions = new ApiOptions();
        app.Configuration.GetSection(ApiOptions.SectionName).Bind(apiOptions);

        var culture = new CultureInfo(apiOptions.Culture);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        app.DescribeApiVersions();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = $"{apiOptions.Name} Documentation";
            var apiVersions = app.DescribeApiVersions();

            foreach (var description in apiVersions)
                options.SwaggerEndpoint(
                    $"../swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        });

        app.MapHealthChecks("/health")
            .ShortCircuit();

        app.MapControllers();

        return app;
    }
}