using MediatR;
using OICTCaseStudy.ApiClient.Litacka.Services;
using OICTCaseStudy.App.Dto;

namespace OICTCaseStudy.App.Query.GetCardValidityById;

public sealed class GetCardValidityByIdQueryHandler(LitackaCardService litackaCardService)
    : IRequestHandler<GetCardValidityByIdQuery, CardValidityDto>
{
    public async Task<CardValidityDto> Handle(GetCardValidityByIdQuery request, CancellationToken cancellationToken)
    {
        var cardStateTask = litackaCardService.RequestCardState(request.CardId, cancellationToken);
        var cardValidityTask = litackaCardService.RequestCardValidity(request.CardId, cancellationToken);

        await Task.WhenAll(cardStateTask, cardValidityTask);

        return new CardValidityDto(cardValidityTask.Result.ValidityEnd, cardStateTask.Result.StateDescription);
    }
}