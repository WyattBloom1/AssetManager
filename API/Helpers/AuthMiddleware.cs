
using AssetManager.Helpers.PasswordHelper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AssetManager.Helper
{
    public class AuthMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        //private readonly PasswordHelper _passwordHelper;

        public AuthMiddleware(ILogger<AuthMiddleware> logger)
        {
            _logger = logger;
            //_passwordHelper = passwordHelper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogTrace("Middleware");
            await next(context);
            //throw new NotImplementedException();
        }
    }
}