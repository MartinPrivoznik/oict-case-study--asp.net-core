using System.Net;
using MediatR;
using OICTCaseStudy.ApiClient.Litacka.Services;
using OICTCaseStudy.App.Dto;
using OICTCaseStudy.App.Exceptions;

namespace OICTCaseStudy.App.Query.GetCardValidityById;

public sealed class GetCardValidityByIdQueryHandler(ILitackaCardService litackaCardService)
    : IRequestHandler<GetCardValidityByIdQuery, CardValidityDto>
{
    public async Task<CardValidityDto> Handle(GetCardValidityByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cardStateRequestTask = litackaCardService.RequestCardState(request.CardId, cancellationToken);
            var cardValidityRequestTask = litackaCardService.RequestCardValidity(request.CardId, cancellationToken);

            await Task.WhenAll(cardStateRequestTask, cardValidityRequestTask);

            return new CardValidityDto(cardValidityRequestTask.Result.ValidityEnd,
                cardStateRequestTask.Result.StateDescription);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            throw new CardNotFoundException(request.CardId);
        }
    }
}