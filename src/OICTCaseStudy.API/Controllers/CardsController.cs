using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OICTCaseStudy.App.Query.GetCardValidityById;

namespace OICTCaseStudy.Api.Controllers;

[ApiVersion(1)]
public class CardsController(ISender sender) : BaseApiController
{
    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetCardInfo([FromRoute]ulong cardNumber, CancellationToken cancellationToken)
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