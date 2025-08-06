using MicroApi.DataLayer.Interface;
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
                res = _salaryService.insert(model); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }

    }
}
