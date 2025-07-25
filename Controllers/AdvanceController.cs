using Microsoft.AspNetCore.Authorization;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using MicroApi.DataLayer.Interface;

namespace MicroApi.Controllers
{
    
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceController : ControllerBase
    {
        private readonly IAdvanceService _advanceService;
        public AdvanceController(IAdvanceService advanceService)
        {
            _advanceService = advanceService;
        }


        [HttpPost]
        [Route("list")]
        public AdvanceLogListResponseData LogList()
        {
            AdvanceLogListResponseData loglist = new AdvanceLogListResponseData();
            try
            {
                loglist = _advanceService.GetAllPayAdvance();
            }
            catch (Exception ex)
            {
                loglist.flag = "0";
                loglist.message = ex.Message;
            }
            return loglist;
        }

        [HttpPost]
        [Route("save")]
        public saveAdvanceResponseData SaveData(saveAdvanceData advData)
        {
            saveAdvanceResponseData res = new saveAdvanceResponseData();

            try
            {

                _advanceService.SaveData(advData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("select/{id:int}")]
        public saveAdvanceData selectPayAdvanceData(int id)
        {
            saveAdvanceData objAdvance = new saveAdvanceData();
            try
            {

                objAdvance = _advanceService.selectPayAdvanceData(id);
            }
            catch (Exception ex)
            {

            }

            return objAdvance;
        }

        [HttpPost]
        [Route("update")]
        public saveAdvanceResponseData UpdateData(saveAdvanceData advData)
        {
            saveAdvanceResponseData res = new saveAdvanceResponseData();

            try
            {

                _advanceService.UpdateData(advData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public saveAdvanceResponseData Delete(int id)
        {
            saveAdvanceResponseData res = new saveAdvanceResponseData();

            try
            {

                _advanceService.DeletePayAdvance(id);
                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("verify")]
        public saveAdvanceResponseData VerifyData(saveAdvanceData advData)
        {
            saveAdvanceResponseData res = new saveAdvanceResponseData();

            try
            {

                _advanceService.VerifyData(advData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("approve")]
        public saveAdvanceResponseData ApproveData(saveAdvanceData advData)
        {
            saveAdvanceResponseData res = new saveAdvanceResponseData();

            try
            {

                _advanceService.ApproveData(advData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }
    }
}
