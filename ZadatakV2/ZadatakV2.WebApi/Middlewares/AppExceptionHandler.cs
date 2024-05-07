using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ZadatakV2.Shared.Exceptions;

namespace ZadatakV2.WebApi.Middlewares
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
                                                    Exception exception, 
                                                    CancellationToken cancellationToken)
        {
            var result = new ProblemDetails();
            switch (exception)
            {
                case ArgumentNullException argumentNullException:
                    result = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Type = argumentNullException.GetType().Name,
                        Title = "An unexpected error occurred",
                        Detail = argumentNullException.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    };                    
                    break;
                case UniqueConstraintViolationException uniqueConstraintException:
                    result = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = uniqueConstraintException.GetType().Name,
                        Title = "Bad request",
                        Detail = uniqueConstraintException.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };
                    break;
                case EntityNotFoundException entityNotFoundException:
                    result = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        Type = entityNotFoundException.GetType().Name,
                        Title = "Not found",
                        Detail = entityNotFoundException.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };
                    break;
                default:
                    result = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = exception.GetType().Name,
                        Title = "An unexpected error occurred",
                        Detail = exception.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };                    
                    break;
            }

            httpContext.Response.StatusCode = (int)result.Status;
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
            return true;
        }
    }
}
