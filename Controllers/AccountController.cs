using JobManagerAPI_v4.Models;
using JobManagerAPI_v4.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobManagerAPI_v4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        /// <summary>
        ///  Returns all the orders of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order List</returns>
        public ActionResult<List<Account>> GetAllAccounts(int ownerId)
        {
            try
            {
                if(ownerId == 0)
                {
                    return Ok(_accountService.GetAllAccounts(0));
                }
                return Ok(_accountService.GetAllAccounts(1));
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return NotFound("Order not found");
            }
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

        [HttpPost]
        public ActionResult<string> AddAccount(Account account)
        {
            Console.WriteLine("Here");
            try
            {
                int accountId = _accountService.CreateAccount(account);
                return StatusCode(201, accountId);
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
    }
}
