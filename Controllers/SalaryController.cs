using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _SalaryService;
        public SalaryController(ISalaryService SalaryService)
        {
            _SalaryService = SalaryService;
        }
        [HttpPost]
        [Route("generate")]
        public GenerateSalaryResponse GenerateSalary(Salary request)
        {
            GenerateSalaryResponse response = new GenerateSalaryResponse();

            try
            {
                List<Salary> salaryList = new List<Salary> { request };
                response = _SalaryService.GenerateSalary(salaryList);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost]
        [Route("list")]
        public SalaryLookupResponse GetSalaryLookup(SalaryLookupRequest request)
        {
            SalaryLookupResponse response = new SalaryLookupResponse();

            try
            {
                response = _SalaryService.GetSalaryLookup(request);
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
