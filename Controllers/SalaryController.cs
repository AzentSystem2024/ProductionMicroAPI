using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using static MicroApi.DataLayer.Service.SalaryService;

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

        [HttpPost]
        [Route("view")]
        public PayrollViewResponse GetPayrollDetails(PayrollViewRequest request)
        {
            PayrollViewResponse response = new PayrollViewResponse();

            try
            {
                response = _SalaryService.GetPayrollDetails(request.PAYDETAIL_ID);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
        [HttpPost]
        [Route("edit")]
        public PayrollResponse UpdateGrossOrDeduction(UpdateItemRequest request)
        {
            PayrollResponse response = new PayrollResponse();

            try
            {
                response = _SalaryService.Edit(request);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }

        [HttpPost]
        [Route("approve")]
        public SalaryApproveResponse CommitGenerateSalary(SalaryApprove request)
        {
            SalaryApproveResponse response = new SalaryApproveResponse();
            try
            {
                response = _SalaryService.CommitGenerateSalary(request);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("delete")]
        public DeleteSalaryResponse DeleteGeneratedSalary(DeleteSalaryRequest request)
        {
            DeleteSalaryResponse response = new DeleteSalaryResponse();
            try
            {
                response = _SalaryService.DeleteGeneratedSalary(request);
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
