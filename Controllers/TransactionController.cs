using AssetManager.Services;
using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Services.Transactions;

namespace AssetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService transactionService)
        {
            _service = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(Transaction transaction)
        {
            try
            {
                int returndId = await _service.Create(transaction);
                return returndId;
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        /// <summary>
        ///  Returns all the orders of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order List</returns>
        public async Task<IEnumerable<Transaction>> GetAll()
        {
            IEnumerable<Transaction> accountTypes = await _service.GetAll();
            return accountTypes;
        }

        [HttpGet("{accountId}")]
        /// <summary>
        ///  Returns the individual order of Customer. Assume we have one customer for now
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>Order</returns>
        public ActionResult<Transaction> GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return NotFound("Account not found");
            }
        }

        [HttpPatch]
        /// <summary>
        ///  Modify the quantity of the food in order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="quantity"></param>
        /// <returns>200</returns>
        public ActionResult<string> Update(Transaction transaction)
        {
            try
            {
                _service.Update(transaction);
                return Ok("Account type updated!");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<bool> DeleteAccount(int id)
        {
            try
            {
                _service.Delete(id);
                return StatusCode(200, true);
            }
            catch
            {
                throw;
            }
        }
    }
}
