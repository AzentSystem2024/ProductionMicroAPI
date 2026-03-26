using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryWPSController : ControllerBase
    {
        private readonly ISalaryWPSService _salaryWPSService;
        public SalaryWPSController(ISalaryWPSService SalaryWPSService)
        {
            _salaryWPSService = SalaryWPSService;
        }
        [HttpPost("wps")]
        public SalaryWPSResponse GetSalaryWPS(SalaryWPSRequest request)
        {
            var res = new SalaryWPSResponse();
            try
            {
                res = _salaryWPSService.GetSalaryWPS(request);
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
