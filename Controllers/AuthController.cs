using AssetManager.Helpers.PasswordHelper;
using AssetManager.Models;
using AssetManager.Repository.SqlServer.Users;
using AssetManager.Services.Users;
using AssetManager.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManager.Controllers
{
    public class AuthenticatedResponse
    {
        public string? AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiration { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly string SecurityKey;

        public AuthController(IPasswordHelper passwordHelper, IUserService userService, IConfiguration configuration)
        {
            _passwordHelper = passwordHelper;
            _userService = userService;
            _configuration = configuration;

            var securityKey = _configuration["SecurityKey"];
            if (securityKey is null)
                throw new InvalidOperationException("Missing JWT Security Key.");

            this.SecurityKey = securityKey;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserLogin user)
        {
            try
            {
                if (user is null || user.UserName is null || user.Password is null)
                {
                    return BadRequest("Invalid client request");
                }

                User userId = await _userService.Create(user.UserName, user.Password);

                if (userId != null)
                {
                    return Ok(userId);
                }
                return Unauthorized();
            } 
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            try
            {
                if (user is null || user.UserName is null)
                    return BadRequest("Invalid client request");

                User userExists = await _userService.GetByUserName(user);
                if (userExists is null)
                    return Unauthorized();
                
                AuthenticatedResponse authenticatedResponse = GenerateTokenAndRefresh(userExists, null);

                // Update the refresh token in the Database
                RefreshTokens refreshToken = new RefreshTokens(authenticatedResponse, _passwordHelper.HashPassword(authenticatedResponse.RefreshToken, ""), "prefix", false);
                await _userService.CreateRefreshToken(refreshToken);

                return Ok(authenticatedResponse);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("refresh"), Authorize]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            try
            {
                // Hash the passed in Refresh token to compare to the stored value in the DB
                string hashedToken = _passwordHelper.HashPassword(refreshToken, "");

                // Get the user information from the current session variable
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userName = User.FindFirst("userName")?.Value;
                if (userId is null)
                    return BadRequest();

                // Verify that the Refresh token is still valid in the database and has not been revoked
                RefreshTokens token = await _userService.VerifyRefreshToken(int.Parse(userId), hashedToken);
                if (token is null)
                    return BadRequest();

                // If the refresh token is still valid, generate both a new refresh and a new auth token
                AuthenticatedResponse authenticatedResponse = GenerateTokenAndRefresh(new User(int.Parse(userId), userName, "", "", ""), null);

                // Update the refresh token in the Database to the newly generated one
                RefreshTokens newToken = new RefreshTokens(authenticatedResponse, _passwordHelper.HashPassword(authenticatedResponse.RefreshToken, ""), "prefix", false);
                await _userService.UpdateRefreshToken(newToken, hashedToken);

                // Return the generated tokens to the user
                return Ok(authenticatedResponse);
            }
            catch
            {
                return BadRequest();
            }
        }

        private AuthenticatedResponse GenerateTokenAndRefresh(User user, List<Claim>? claims)
        {
            // Define the JWT Payload options in a dictionary object
            // TODO: Insert a User object into a payload parameter
            Dictionary<string, string> payloadOptions = new Dictionary<string, string>();
            payloadOptions.Add("sub", user.UserId.ToString());
            payloadOptions.Add("userName", user.UserName);
            payloadOptions.Add("role", "Test Role");

            // Define the expiration date for the Access Token (shorter)
            DateTime expirationDate = DateTime.Now.AddMinutes(120);
            string tokenString = GenerateJwtToken(payloadOptions, expirationDate, null);

            // Define the expiration date for the Refresh Token (longer)
            DateTime refreshExpiration = DateTime.Now.AddDays(7);
            string refreshToken = GenerateJwtToken(payloadOptions, refreshExpiration, null);

            return new AuthenticatedResponse
            {
                RefreshToken = refreshToken,
                AccessToken = tokenString,
                AccessTokenExpiration = refreshExpiration,
                UserId = user.UserId,
                UserName = user.UserName
            };
        }

        private string GenerateJwtToken(Dictionary<string, string> payload, DateTime expirationDate, List<Claim>? claims)
        {
            if (claims is null) {
                claims = new List<Claim>();
            }

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: claims,
                expires: expirationDate,
                signingCredentials: signingCredentials
            );
            
            // Add the passed-in and custom payload options
            foreach (var item in payload) {
                tokenOptions.Payload[item.Key] = item.Value;
            }

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

    }
}
