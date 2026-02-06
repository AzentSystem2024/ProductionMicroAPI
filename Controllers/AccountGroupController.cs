using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [Route("api/accountGroup")]
    [ApiController]
    public class AccountGroupController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountGroupController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost]
        [Route("Insert")]
        public AccountResponse Insert(Account account)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                res = _accountService.Insert(account);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;

        }
        [HttpPost]
        [Route("update")]

        public AccountResponse Update(AccountUpdate account)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                res = _accountService.Update(account);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public AccountResponse select(int id)
        {
            AccountResponse res = new AccountResponse();
            try
            {

                res = _accountService.GetAccountById(id);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }

        [HttpPost]
        [Route("list")]
        public AccountListResponse AccountLogList()
        {

            AccountListResponse res = new AccountListResponse();
            try
            {
                res = _accountService.GetLogList();

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public AccountResponse Delete(int id)
        {
            AccountResponse res = new AccountResponse();
            try
            {
                res = _accountService.DeleteAccountData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
