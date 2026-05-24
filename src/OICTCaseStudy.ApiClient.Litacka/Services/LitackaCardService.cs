using OICTCaseStudy.ApiClient.Litacka.Core.Response;
using OICTCaseStudy.ApiClient.Litacka.HttpClient;

namespace OICTCaseStudy.ApiClient.Litacka.Services;

public class LitackaCardService(LitactkaHttpClient httpClient)
{
    public Task<CardValidityResponse> RequestCardValidity(ulong cardId, CancellationToken cancellationToken = default)
    {
        return httpClient.Get<CardValidityResponse>($"/cards/{cardId}/validity", cancellationToken);
    }

    public Task<CardStateResponse> RequestCardState(ulong cardId, CancellationToken cancellationToken = default)
    {
        return httpClient.Get<CardStateResponse>($"/cards/{cardId}/state", cancellationToken);
    }
}