using Serilog;

namespace JwT_with_RefreshToken.Middleware
{
    public class GlobalMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public GlobalMiddleware(Serilog.ILogger logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger.ForContext<GlobalMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unhandled exception: {Message}", ex.Message);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
