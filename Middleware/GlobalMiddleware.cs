namespace JwT_with_RefreshToken.Middleware
{
    public class GlobalMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalMiddleware> _logger;

        public GlobalMiddleware(RequestDelegate next, ILogger<GlobalMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
