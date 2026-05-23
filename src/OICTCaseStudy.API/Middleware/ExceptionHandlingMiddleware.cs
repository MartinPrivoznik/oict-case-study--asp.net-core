using System.Text.Json;
using OICTCaseStudy.Api.Core;

namespace OICTCaseStudy.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotImplementedException ex)
        {
            await WriteApiResponse(
                context,
                StatusCodes.Status501NotImplemented,
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "Method not yet implemented."
                }, ex);
        }
        catch (Exception ex)
        {
            await WriteApiResponse(
                context,
                StatusCodes.Status500InternalServerError,
                new ApiResponse<object>
                {
                    Success = false,
                    Message = "Method not yet implemented."
                }, ex);
        }
    }

    private async Task WriteApiResponse(HttpContext context, int status, ApiResponse<object> payload,
        Exception ex)
    {
        if (context.Response.HasStarted) throw ex;

        context.Response.Clear();
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload, JsonOptions));
    }
}