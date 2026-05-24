using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OICTCaseStudy.Api.Core;
using OICTCaseStudy.App.Dto;
using OICTCaseStudy.App.Query.GetCardValidityById;

namespace OICTCaseStudy.Api.Controllers;

[Authorize]
[ApiVersion(1)]
public class CardsController(ISender sender) : BaseApiController
{
    /// <summary>
    ///     Retrieves the validity information of a card based on its number.
    /// </summary>
    /// <param name="cardNumber"> Card number </param>
    [HttpGet("{cardNumber}")]
    public async Task<ActionResult<ApiResponse<CardValidityDto>>> GetCardInfo([FromRoute] ulong cardNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            var cardValidity = await sender.Send(new GetCardValidityByIdQuery(cardNumber), cancellationToken);
            return SuccessResponse(cardValidity);
        }
        catch (InvalidOperationException ex)
        {
            return ConflictResponse(ex.Message);
        }
    }
}