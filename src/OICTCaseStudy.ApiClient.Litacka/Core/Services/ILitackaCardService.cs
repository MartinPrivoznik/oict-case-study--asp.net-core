using OICTCaseStudy.ApiClient.Litacka.Core.Response;

namespace OICTCaseStudy.ApiClient.Litacka.Services;

public interface ILitackaCardService
{
    Task<CardValidityResponse> RequestCardValidity(ulong cardId, CancellationToken cancellationToken = default);
    Task<CardStateResponse> RequestCardState(ulong cardId, CancellationToken cancellationToken = default);
}
