using AssetManager.Helpers.PasswordHelper;
using AssetManager.Models;
using AssetManager.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AssetManager.Controllers
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;


        public AuthController(IPasswordHelper passwordHelper, IUserService userService, IConfiguration configuration)
        {
            _passwordHelper = passwordHelper;
            _userService = userService;
            _configuration = configuration;
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

                //if (user.UserName == "johndoe" && user.Password == "def@123")
                //{
                //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey12343221234212342@34567432345"));
                //    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                //    var tokeOptions = new JwtSecurityToken(
                //        issuer: "https://localhost:5001",
                //        audience: "https://localhost:5001",
                //        claims: new List<Claim>(),
                //        expires: DateTime.Now.AddMinutes(5),
                //        signingCredentials: signinCredentials
                //    );
                //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                //    return Ok(new AuthenticatedResponse { Token = tokenString });
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
                

                // Define the JWT Payload options in a dictionary object
                Dictionary<string, string> payloadOptions = new Dictionary<string, string>();
                payloadOptions.Add("sub", userExists.UserId.ToString());
                payloadOptions.Add("userName", userExists.UserName);

                string tokenString = GenerateJwtToken(_configuration["SecurityKey"], payloadOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });

                
            }
            catch
            {
                return BadRequest();
            }
        }

        private static string GenerateJwtToken(string SecurityString, Dictionary<string, string> payload)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityString));
            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(120),
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
