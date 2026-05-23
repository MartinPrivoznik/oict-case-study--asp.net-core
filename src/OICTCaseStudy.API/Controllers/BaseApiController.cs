using Microsoft.AspNetCore.Mvc;
using OICTCaseStudy.Api.Core;

namespace OICTCaseStudy.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected ActionResult SuccessResponse(object? data = null)
    {
        return Ok(new ApiResponse<object?>
        {
            Data = data,
            Success = true
        });
    }

    protected ActionResult ConflictResponse(string message)
    {
        return Conflict(new ApiResponse<object?>
        {
            Message = message,
            Success = false
        });
    }

    protected ActionResult NotFoundResponse(string message)
    {
        return NotFound(new ApiResponse<object?>
        {
            Message = message,
            Success = false
        });
    }
}