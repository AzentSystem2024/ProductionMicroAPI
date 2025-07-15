using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimeSheetService _timeSheetService;
        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }


        [HttpPost]
        [Route("list")]
        public TimeSheetLogListResponseData LogList()
        {
            TimeSheetLogListResponseData loglist = new TimeSheetLogListResponseData();
            try
            {
                loglist = _timeSheetService.GetAllTimeSheet();
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
        public saveTimeSheetResponseData SaveData(saveTimeSheetData timesheetData)
        {
            saveTimeSheetResponseData res = new saveTimeSheetResponseData();

            try
            {

                _timeSheetService.SaveData(timesheetData);

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
        public saveTimeSheetData selectTimeSheetData(int id)
        {
            saveTimeSheetData objTs = new saveTimeSheetData();
            try
            {

                objTs = _timeSheetService.selectTimeSheetData(id);
            }
            catch (Exception ex)
            {

            }

            return objTs;
        }

        [HttpPost]
        [Route("update")]
        public saveTimeSheetResponseData UpdateData(saveTimeSheetData timesheetData)
        {
            saveTimeSheetResponseData res = new saveTimeSheetResponseData();

            try
            {

                _timeSheetService.UpdateData(timesheetData);

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
        public saveTimeSheetResponseData Delete(int id)
        {
            saveTimeSheetResponseData res = new saveTimeSheetResponseData();

            try
            {

                _timeSheetService.DeleteTimeSheet(id);
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
        public saveTimeSheetResponseData VerifyData(saveTimeSheetData timesheetData)
        {
            saveTimeSheetResponseData res = new saveTimeSheetResponseData();

            try
            {

                _timeSheetService.VerifyData(timesheetData);

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
        public saveTimeSheetResponseData ApproveData(saveTimeSheetData timesheetData)
        {
            saveTimeSheetResponseData res = new saveTimeSheetResponseData();

            try
            {

                _timeSheetService.ApproveData(timesheetData);

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
        [Route("ListTimesheet")]
        public IActionResult GetTimeSheetByCompanyAndMonth([FromBody] TimeSheetRequest request)
        {
            TimeSheetHeaderListResponseData logList = new TimeSheetHeaderListResponseData();
            try
            {
                DateTime month = DateTime.ParseExact(request.Month, "MMMMyyyy", CultureInfo.InvariantCulture);
                logList = _timeSheetService.GetTimeSheetByCompanyAndMonth(request.CompanyId, month);
            }
            catch (Exception ex)
            {
                logList.flag = "0";
                logList.message = ex.Message;
                return BadRequest(logList);
            }
            return Ok(logList);
        }

        [HttpPost]
        [Route("approvetimesheet")]
        public IActionResult ApproveTimeSheets([FromBody] ApproveRequest request)
        {
            var response = _timeSheetService.ApproveTimeSheets(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("salary-pending")]
        public IActionResult GetSalaryPendingTimeSheets([FromBody] TimeSheetRequest request)
        {
            var response = _timeSheetService.GetSalaryPendingTimeSheets(request);
            return Ok(response);
        }

    }
}
