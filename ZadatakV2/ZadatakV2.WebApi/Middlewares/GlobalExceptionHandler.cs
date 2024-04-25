using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace ZadatakV2.WebApi.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly HttpContext _context;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly string _contentType = "application/json";

        public GlobalExceptionHandler(HttpContext context, ILogger<GlobalExceptionHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task OnException(Exception exception)
        {
            PrepareErrorResponse();
            ProblemDetails problemDetails = CreateProblemDetails(exception);
            LogException(exception);
            return _context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }

        private void PrepareErrorResponse()
        {
            _context.Response.Clear();
            _context.Response.ContentType = _contentType;
        }

        private ProblemDetails CreateProblemDetails(Exception exception)
        {
            switch (exception)
            {
                case DbUpdateException dbe:
                    _context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return CreateProblemDetails(exception, "Bad request.");                
                default:
                    _context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return CreateProblemDetails(exception, "Something went wrong.");
            }
        }

        private ProblemDetails CreateProblemDetails(Exception exception, string title) =>
            new()
            {
                Title = title,
                Status = _context.Response.StatusCode,
                Detail = exception.Message,
                Type = exception.GetType().ToString()
            };

        private void LogException(Exception exception)
            => _logger.LogError(exception, $"Global error handler: {exception.Message}");
    }
}
