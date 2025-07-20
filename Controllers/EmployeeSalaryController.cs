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
        public EmployeeListResponse List([FromBody] EmployeeSalaryRequest request)
        {
            EmployeeListResponse res = new EmployeeListResponse();

            try
            {
                res = _EmployeeSalaryService.GetAllEmployeeSalaries(request.EMP_ID,request.COMPANY_ID);
            }
            catch (Exception ex)
            {
                res = new EmployeeListResponse
                {
                    flag = 0,
                    Message = ex.Message,
                    Data = null
                };
            }

            return res;
        }


        [HttpPost]
        [Route("select/{id:int}")]
        public EmployeeSalaryUpdate Select(int id)
        {
            EmployeeSalaryUpdate salaryHead = new EmployeeSalaryUpdate();

            try
            {
                salaryHead = _EmployeeSalaryService.GetItem(id);
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
                res.data = _EmployeeSalaryService.GetItem(ID);
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
        public IActionResult EditData([FromBody] EmployeeSalaryUpdate salary)
        {
            try
            {
                Int32 ID = _EmployeeSalaryService.EditData(salary);
                var updatedSalary = _EmployeeSalaryService.GetItem(ID);
                return Ok(new { flag = "1", message = "Success", data = updatedSalary });
            }
            catch (Exception ex)
            {
                return BadRequest(new { flag = "0", message = ex.Message });
            }
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public EmployeeSalaryResponse Delete(int id)
        {
            EmployeeSalaryResponse res = new EmployeeSalaryResponse();

            try
            {
                _EmployeeSalaryService.DeleteEmployeeSalary(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _EmployeeSalaryService.GetItem(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
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
                res = _EmployeeSalaryService.GetEmployeeSalarySettings(request.FilterAction);
            }
            catch (Exception ex)
            {
                res = new EmployeeSalarySettingsListResponse { flag = 0, Message = ex.Message, Data = null };
            }
            return res;
        }
    }
}
