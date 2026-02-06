using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/AcReports/TrialBalance")]
    public class TrialBalanceReportController : ControllerBase
    {
        private readonly ITrialBalanceReportService _trialBalanceReportService;

        public TrialBalanceReportController(ITrialBalanceReportService trialBalanceReportService)
        {
            _trialBalanceReportService = trialBalanceReportService;
        }

        [HttpPost]
        public IActionResult GetTrialBalanceReport([FromBody] ReportRequest request)
        {
            try
            {
                // Parse the dates using the format "yyyy-MM-dd" to ignore the time component
                DateTime dateFrom = DateTime.ParseExact(request.DateFrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime dateTo = DateTime.ParseExact(request.DateTo, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var reportData = _trialBalanceReportService.GetTrialBalanceReport(request.CompanyId, request.FinId, dateFrom, dateTo);
                return Ok(new { flag = 1, message = "Success", data = reportData });
            }
            catch (Exception ex)
            {
                return BadRequest(new { flag = 0, message = ex.Message });
            }
        }
    }
}

