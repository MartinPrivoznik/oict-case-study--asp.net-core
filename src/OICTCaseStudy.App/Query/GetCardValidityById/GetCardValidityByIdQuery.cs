using MediatR;
using OICTCaseStudy.App.Dto;

namespace OICTCaseStudy.App.Query.GetCardValidityById;

public sealed record GetCardValidityByIdQuery(ulong CardId) : IRequest<CardValidityDto>;