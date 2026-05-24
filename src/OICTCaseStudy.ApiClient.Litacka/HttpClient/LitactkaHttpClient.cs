using System.Text.Json;

namespace OICTCaseStudy.ApiClient.Litacka.HttpClient;

public class LitactkaHttpClient(System.Net.Http.HttpClient httpClient)
{
    public async Task<T> Get<T>(string url, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<T>(content) ??
               throw new InvalidOperationException("Failed to deserialize response.");
    }
}