using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using OICTCaseStudy.Api.Configuration;

namespace OICTCaseStudy.Api.Authentication;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IOptions<ApiOptions> apiOptions)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "ApiKey";
    private const string HeaderName = "X-Api-Key";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (string.IsNullOrEmpty(apiOptions.Value.ApiKey))
            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(), SchemeName)));

        if (!Request.Headers.TryGetValue(HeaderName, out var headerValues))
            return Task.FromResult(AuthenticateResult.NoResult());

        if (headerValues.ToString() != apiOptions.Value.ApiKey)
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));

        var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
        var identity = new ClaimsIdentity(claims, SchemeName);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), SchemeName);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
