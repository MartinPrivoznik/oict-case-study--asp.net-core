using MediatR;
using OICTCaseStudy.App.Dto;

namespace OICTCaseStudy.App.Query.GetCardValidityById;

public sealed class GetCardValidityByIdQueryHandler : IRequestHandler<GetCardValidityByIdQuery, CardValidityDto>
{
    public Task<CardValidityDto> Handle(GetCardValidityByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}