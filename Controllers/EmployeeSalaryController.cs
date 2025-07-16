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
        public EmployeeListResponse List()
        {
            EmployeeListResponse res = new EmployeeListResponse();

            try
            {
                res = _EmployeeSalaryService.GetAllEmployeeSalaries();
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
        public EmployeeSalaryResponse EditData(EmployeeSalaryUpdate salary)
        {
            EmployeeSalaryResponse res = new EmployeeSalaryResponse();

            try
            {
                Int32 ID = _EmployeeSalaryService.EditData(salary);

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
    }
}
