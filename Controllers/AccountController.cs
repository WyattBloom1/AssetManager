using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Services.Accounts;

namespace AssetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            //_accountService = accountService;
            _accountService = accountService;
        }

        [HttpGet]
        /// <summary>
        ///  Returns all the orders of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order List</returns>
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            IEnumerable<Account> accounts = await _accountService.GetAllAccounts();
            return accounts;
        }

        [HttpGet("{accountId}")]
        /// <summary>
        ///  Returns the individual order of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order</returns>
        public ActionResult<Account> GetAccountById(int accountId)
        {
            try
            {
                return Ok(_accountService.GetAccountById(accountId));
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return NotFound("Account not found");
            }
        }

        [HttpGet("{accountId}/History")]
        /// <summary>
        ///  Returns the individual order of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order</returns>
        public async Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId)
        {
            try
            {
                IEnumerable<AccountHistory> accountHistory = await _accountService.GetAccountHistory(accountId);
                return accountHistory;
            }
            catch (Exception ex)
            {
                throw;
                //Log the error i.e., ex.Message
                //return NotFound("Account not found");
            }
        }

        [HttpPost("{accountId}/SetBalance")]
        public async Task<ActionResult<int>> AddAccount(int accountId, decimal accountBalance)
        {
            try
            {
                await _accountService.SetAccountBalance(accountId, accountBalance);
                return accountId;

                //int accountId = _accountService.CreateAccount(account);
                //return StatusCode(201, accountId);
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        /// <summary>
        ///  Modify the quantity of the food in order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="quantity"></param>
        /// <returns>200</returns>
        public ActionResult<string> ModifyAccountBalance(int accountId, int accountBalance)
        {
            try
            {
                _accountService.SetAccountBalance(accountId, accountBalance);
                return Ok("Account balance updated!");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<bool> DeleteAccount(int accountId)
        {
            try
            {
                _accountService.DeleteAccount(accountId);
                return StatusCode(200, true);
            }
            catch
            {
                throw;
            }
        }
    }
}
