namespace OICTCaseStudy.ApiClient.Litacka.Configuration;

public sealed class LitackaApiClientOptions
{
    public const string SectionName = "LitackaApiClientOptions";

    public string UrlBase { get; set; } = "https://api.litacka.com/";
    public string ApiClientName { get; set; } = "LitackaApiClient";
    public int RetryMaxAttempts { get; set; } = 3;
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);
}