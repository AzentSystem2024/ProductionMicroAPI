using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeSalaryController : ControllerBase
    {
        private readonly IEmployeeSalaryService _EmployeeSalaryService;
        public EmployeeSalaryController(IEmployeeSalaryService EmployeeSalaryService)
        {
            _EmployeeSalaryService = EmployeeSalaryService;
        }
        [HttpPost]
        [Route("list")]
        public IActionResult List([FromBody] EmployeeSalaryRequest request)
        {
            try
            {
                var response = _EmployeeSalaryService.GetAllEmployeeSalaries(request.EMP_ID,request.COMPANY_ID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }


        [HttpPost]
        [Route("select")]
        public EmployeeListResponse Select([FromBody]EmployeeSalarySelectRequest request)
        {
            EmployeeListResponse salaryHead = new EmployeeListResponse();

            try
            {
                salaryHead = _EmployeeSalaryService.GetItem(request.EMP_ID,request.EFFECT_FROM);
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return salaryHead;
        }

        [HttpPost]
        [Route("save")]
        public EmployeeSalaryResponse SaveData(EmployeeSalarySave salary)
        {
            EmployeeSalaryResponse res = new EmployeeSalaryResponse();

            try
            {
                Int32 ID = _EmployeeSalaryService.SaveData(salary);
                //var updatedList = _EmployeeSalaryService.GetAllEmployeeSalaries();

                res.flag = "1";
                res.message = "Success";
                //res.data = _EmployeeSalaryService.GetItem(ID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("edit")]
        public IActionResult EditData([FromBody] EmployeeSalarySave salaryUpdate)
        {
            try
            {   
                Int32 ID = _EmployeeSalaryService.EditData(salaryUpdate);
                //var updatedSalary = _EmployeeSalaryService.GetItem(ID);
                return Ok(new { flag = "1", message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { flag = "0", message = ex.Message });
            }
        }

        [HttpPost]
        [Route("delete")]
        public EmployeeListResponse Delete([FromBody] EmployeeSalarySelectRequest request)
        {
            EmployeeListResponse res = new EmployeeListResponse();

            try
            {
                _EmployeeSalaryService.DeleteEmployeeSalary(request.EMP_ID,request.EFFECT_FROM);
                //_EmployeeSalaryService.GetItem(id);
                res.flag = 1;
                res.Message = "Deleted successfully";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;

            }
            return res;
        }

        [HttpPost]
        [Route("ListSalarySettings")]
        public EmployeeSalarySettingsListResponse ListSalarySettings([FromBody] EmployeeSalaryFilterRequest request)
        {
            EmployeeSalarySettingsListResponse res = new EmployeeSalarySettingsListResponse();
            try
            {
                res = _EmployeeSalaryService.GetEmployeeSalarySettings(request.FilterAction, request.CompanyId);
            }
            catch (Exception ex)
            {
                res = new EmployeeSalarySettingsListResponse { flag = 0, Message = ex.Message, Data = null };
            }
            return res;
        }
    }
}
