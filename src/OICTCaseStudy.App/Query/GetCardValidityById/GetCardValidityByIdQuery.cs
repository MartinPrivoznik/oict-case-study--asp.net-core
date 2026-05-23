using MediatR;
using OICTCaseStudy.App.Dto;

namespace OICTCaseStudy.App.Query.GetCardValidityById;

public sealed record GetCardValidityByIdQuery(Guid cardId) : IRequest<CardValidityDto>;