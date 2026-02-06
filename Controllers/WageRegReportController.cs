using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WageRegReportController: ControllerBase
    {
        private readonly IWageRegReportService _wageRegReportService;
        public WageRegReportController(IWageRegReportService wageRegReportService)
        {
            _wageRegReportService = wageRegReportService;
        }
        [HttpPost("wagereport")]
        public WageReportResponse GetWageReport(WageReportRequest request)
        {
            var res = new WageReportResponse();
            try
            {
                res = _wageRegReportService.GetWageReport(request);
                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }
            return res;
        }
    }

}

