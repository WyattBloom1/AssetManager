
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

        public static string GenerateJwtToken()
        {
            DateTime value = DateTime.Now.AddMinutes(20.0);
            byte[] bytes = Encoding.ASCII.GetBytes("MIIBrTCCAaGg ...");
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = value,
                SigningCredentials = signingCredentials
            };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}