using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Helpers.PasswordHelper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AssetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IPasswordHelper _passwordHelper;

        public UserController(IPasswordHelper passwordHelper)
        {
            //_accountService = accountService;
            _passwordHelper = passwordHelper;
        }

        [HttpPost]
        public ActionResult GetPassHash(string password)
        {
            //return Ok("Hashed Pass: " + _passwordHelper.HashPassword(password));
            return Ok("SUCCESS");
        }

        [HttpPost("TestPass")]
        public ActionResult TestPass(string password, string hashedPass, string salt)
        {
            return Ok("Hashed Pass: " + _passwordHelper.VerifyPassword(password, hashedPass, salt));
            //return Ok("SUCCESS");
        }

        [HttpGet("profile")]
        [Authorize] // Ensures the user is authenticated
        public IActionResult GetProfile()
        {
            // Access claims directly from the User object
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst("userName")?.Value; // Custom claim name
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { UserId = userId, Name = userName, Role = userRole });
        }

        //[HttpGet("{accountId}")]
        //public ActionResult<Account> GetAccountById(int accountId)
        //{
        //    try
        //    {
        //        return Ok('Service Not Implemented');
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log the error i.e., ex.Message
        //        return NotFound("Account not found");
        //    }
        //}

        //[HttpGet("{accountId}/History")]
        /// <summary>
        ///  Returns the individual order of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order</returns>
        //public async Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId)
        //{
        //    try
        //    {
        //        IEnumerable<AccountHistory> accountHistory = await _accountService.GetAccountHistory(accountId);
        //        return accountHistory;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //        //Log the error i.e., ex.Message
        //        //return NotFound("Account not found");
        //    }
        //}

        //[HttpPost("{accountId}/SetBalance")]
        //public async Task<ActionResult<int>> AddAccount(int accountId, decimal accountBalance)
        //{
        //    try
        //    {
        //        await _accountService.SetAccountBalance(accountId, accountBalance);
        //        return accountId;

        //        //int accountId = _accountService.CreateAccount(account);
        //        //return StatusCode(201, accountId);
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log the error i.e., ex.Message
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPatch]
        //public ActionResult<string> ModifyAccountBalance(int accountId, int accountBalance)
        //{
        //    try
        //    {
        //        _accountService.SetAccountBalance(accountId, accountBalance);
        //        return Ok("Account balance updated!");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log the error i.e., ex.Message
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete]
        //public ActionResult<bool> DeleteAccount(int accountId)
        //{
        //    try
        //    {
        //        _accountService.DeleteAccount(accountId);
        //        return StatusCode(200, true);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}
