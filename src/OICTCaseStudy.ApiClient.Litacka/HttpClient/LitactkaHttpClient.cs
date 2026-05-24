using System.Text.Json;
using Microsoft.Extensions.Options;
using OICTCaseStudy.ApiClient.Litacka.Configuration;

namespace OICTCaseStudy.ApiClient.Litacka.HttpClient;

public class LitactkaHttpClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public LitactkaHttpClient(IHttpClientFactory httpClientFactory, IOptions<LitackaApiClientOptions> options)
    {
        _httpClient = httpClientFactory.CreateClient(options.Value.ApiClientName);
    }

    public async Task<T> Get<T>(string url, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(content) ??
               throw new InvalidOperationException("Failed to deserialize response.");
    }
}