using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryPaymentController : ControllerBase
    {
        private readonly ISalaryPaymentService _salaryService;

        public SalaryPaymentController(ISalaryPaymentService salaryService)
        {
            _salaryService = salaryService;
        }
        [HttpPost]
        [Route("insert")]
        public SalaryPaymentResponse Insert(SalaryPayment model)
        {
            SalaryPaymentResponse res = new SalaryPaymentResponse();

            try
            {
                res = _salaryService.Insert(model); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public SalaryPendingResponse GetPendingSalaryList(SalaryPendingRequest request)
        {
            SalaryPendingResponse res = new SalaryPendingResponse();

            try
            {
                res = _salaryService.GetPendingSalaryList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
                res.data = null;
            }

            return res;
        }

    }
}
