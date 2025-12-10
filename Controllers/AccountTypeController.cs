using AssetManager.Models;
using Microsoft.AspNetCore.Mvc;
using AssetManager.Services.AccountTypes;
using System;

namespace AssetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountTypeController : Controller
    {
        private readonly IAccountTypeService _service;

        public AccountTypeController(IAccountTypeService accountTypeService)
        {
            _service = accountTypeService;
        }

        //private readonly IAccountService _accountService;

        //public AccountController(IAccountService accountService)
        //{
        //    //_accountService = accountService;
        //    _accountService = accountService;
        //}

        [HttpPost]
        public async Task<ActionResult<int>> Create(BaseAccountType accountType)
        {
            try
            {
                AccountType newAccount = (AccountType)accountType;
                int returndId = await _service.Create(newAccount);
                return returndId;
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<AccountType>> GetAll()
        {
            IEnumerable<AccountType> accountTypes = await _service.GetAll();
            return accountTypes;
        }

        [HttpGet("{accountTypeId}")]
        public ActionResult<Account> GetById(int accountTypeId)
        {
            try
            {
                return Ok(_service.GetById(accountTypeId));
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return NotFound("Account not found");
            }
        }

        [HttpPatch]
        public ActionResult<string> Update(AccountType accountType)
        {
            try
            {
                //accountType.TypeId = typeId;
                _service.Update(accountType);
                return Ok("Account type updated!");
            }
            catch (Exception ex)
            {
                //Log the error i.e., ex.Message
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<bool> DeleteAccount(int accountTypeId)
        {
            try
            {
                _service.Delete(accountTypeId);
                return StatusCode(200, true);
            }
            catch
            {
                throw;
            }
        }
    }
}
