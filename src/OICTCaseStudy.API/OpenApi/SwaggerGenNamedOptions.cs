using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using OICTCaseStudy.Api.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OICTCaseStudy.Api.OpenApi;

public class SwaggerGenNamedOptions(
    IApiVersionDescriptionProvider apiVersionDescriptionProvider,
    IOptions<ApiOptions> apiOptions)
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            options.SwaggerDoc(
                description.GroupName,
                CreateVersionInfo(description));
    }

    private OpenApiInfo CreateVersionInfo(
        ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = $"{apiOptions.Value.Name} {description.GroupName}",
            Version = description.ApiVersion.ToString()
        };
        return info;
    }
}