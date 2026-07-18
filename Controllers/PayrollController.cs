using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost]
        [Route("GetPayroll")]
        public PayrollReportResponse GetPayrollReport(PayrollReportRequest request)
        {
            PayrollReportResponse response = new PayrollReportResponse();

            try
            {
                response = _payrollService.GetPayrollReport(request);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost]
        [Route("GetPayrollOT")]
        public PayrollOTResponse GetPayrollOTReport(PayrollReportRequest request)
        {
            PayrollOTResponse response = new PayrollOTResponse();

            try
            {
                response = _payrollService.GetPayrollOTReport(request);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}