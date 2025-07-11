using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using System;
using System.Collections.Generic;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Trial_BalanceReportController : ControllerBase
    {
        private readonly ITrialBalanceReportService _trialBalanceReportService;

        public Trial_BalanceReportController(ITrialBalanceReportService trialBalanceReportService)
        {
            _trialBalanceReportService = trialBalanceReportService;
        }

        [HttpPost]
        public IActionResult GetTrialBalanceReport([FromQuery] int companyId, [FromQuery] int finId, [FromQuery] string dateFrom, [FromQuery] string dateTo)
        {
            try
            {
                DateTime parsedDateFrom = DateTime.Parse(dateFrom);
                DateTime parsedDateTo = DateTime.Parse(dateTo);

                List<TrialBalanceReport> reportData = _trialBalanceReportService.GetTrialBalanceReport(companyId, finId, parsedDateFrom, parsedDateTo);
                return Ok(new { flag = 1, message = "Success", data = reportData });
            }
            catch (Exception ex)
            {
                return BadRequest(new { flag = 0, message = ex.Message });
            }
        }
    }
}
