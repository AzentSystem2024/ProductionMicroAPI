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
    public class PaySlipReportController : ControllerBase
    {
        private readonly IPaySlipReportService _paySlipReportService;
        public PaySlipReportController(IPaySlipReportService paySlipReportService)
        {
            _paySlipReportService = paySlipReportService;
        }
        [HttpPost("payslip")]
        public PayslipReportResponse GetPayslipReport(PayslipReportRequest request)
        {
            var res = new PayslipReportResponse();
            try
            {
                res = _paySlipReportService.GetPayslipReport(request);
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
