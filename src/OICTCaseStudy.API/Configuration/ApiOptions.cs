namespace OICTCaseStudy.Api.Configuration;

public sealed class ApiOptions
{
    public const string SectionName = "ApiOptions";

    public string Name { get; set; } = "Api";
    public string Culture { get; set; } = "cs-CZ";
}