using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace OICTCaseStudy.Tests;

public class CardEndpointIntegrationTests
{
    private const string ApiKey = "test-key";

    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder.ConfigureAppConfiguration((_, config) =>
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ApiOptions:ApiKey"] = ApiKey
                })));
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetCard_WithoutApiKey_ReturnsUnauthorized()
    {
        var response = await _client.GetAsync("/api/v1/cards/12345");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetCard_WithNonNumericCardNumber_ReturnsBadRequest()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/cards/not-a-number");
        request.Headers.Add("X-Api-Key", ApiKey);
        var response = await _client.SendAsync(request);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
