
namespace AssetManager.Helper
{
    public class AuthMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public AuthMiddleware(ILogger<AuthMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogTrace("Middleware");
            await next(context);
            //throw new NotImplementedException();
        }
    }
}