namespace ZadatakV2.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                GlobalExceptionHandler globalExceptionHandler = new(context, _logger);
                await globalExceptionHandler.OnException(ex);
            }
        }
    }
}
