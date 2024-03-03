using iPractice.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;
    
    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }
    
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            NotFoundException => new NotFoundObjectResult(context.Exception.Message),
            DomainException => new BadRequestObjectResult(context.Exception.Message),
            _ => new ObjectResult("Error occurred while processing the request.") { StatusCode = StatusCodes.Status500InternalServerError }
        };

        _logger.LogError(context.Exception, context.Exception.Message);
    }
}