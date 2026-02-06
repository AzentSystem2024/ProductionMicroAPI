using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [Route("api/accountHead")]
    [ApiController]
    public class AccountHeadController : ControllerBase
    {
        private readonly IAccountHeadService _accountHeadService;
        public AccountHeadController(IAccountHeadService accountHeadService)
        {
            _accountHeadService = accountHeadService;
        }
        [HttpPost]
        [Route("Insert")]
        public AccountHeadResponse Insert(AccountHead accountHead)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                res = _accountHeadService.Insert(accountHead);
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
        public AccountHeadResponse Update(AccountHeadUpdate accountHead)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                res = _accountHeadService.Update(accountHead);
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
        public AccountHeadResponse select(int id)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {

                res = _accountHeadService.GetAccountHeadById(id);

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
        public AccountHeadListResponse AccountHeadLogList()
        {

            AccountHeadListResponse res = new AccountHeadListResponse();
            try
            {

                res = _accountHeadService.GetLogList();
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
        public AccountHeadResponse Delete(int id)
        {
            AccountHeadResponse res = new AccountHeadResponse();
            try
            {
                res = _accountHeadService.DeleteAccountHeadData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("getlist")]
        public AccountHeadListResponse GetList()
        {

            AccountHeadListResponse res = new AccountHeadListResponse();
            try
            {

                res = _accountHeadService.GetList();
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
