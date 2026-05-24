using MediatR;
using OICTCaseStudy.ApiClient.Litacka.Services;
using OICTCaseStudy.App.Dto;

namespace OICTCaseStudy.App.Query.GetCardValidityById;

public sealed class GetCardValidityByIdQueryHandler(LitackaCardService litackaCardService)
    : IRequestHandler<GetCardValidityByIdQuery, CardValidityDto>
{
    public async Task<CardValidityDto> Handle(GetCardValidityByIdQuery request, CancellationToken cancellationToken)
    {
        var cardStateRequestTask = litackaCardService.RequestCardState(request.CardId, cancellationToken);
        var cardValidityRequestTask = litackaCardService.RequestCardValidity(request.CardId, cancellationToken);

        await Task.WhenAll(cardStateRequestTask, cardValidityRequestTask);

        return new CardValidityDto(cardValidityRequestTask.Result.ValidityEnd,
            cardStateRequestTask.Result.StateDescription);
    }
}