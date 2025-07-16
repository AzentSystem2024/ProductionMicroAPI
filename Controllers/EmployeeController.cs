using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("list")]
        public List<Employee> List([FromBody] CompanyRequest request)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                
                employees = _employeeService.GetAllEmployees(request.CompanyId);
            }
            catch (Exception ex)
            {
            }
            return employees.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Employee Select(int id)
        {
            Employee objEmployees = new Employee();
            try
            {
                
                objEmployees = _employeeService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objEmployees;
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest(new { Message = "Invalid employee data" });
            }

            try
            {
                int employeeId = _employeeService.SaveEmployee(employee);
                return Ok(new { Id = employeeId, Message = "Employee saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while saving the employee", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] EmployeeUpdate employee)
        {
            if (employee == null || employee.ID <= 0)
            {
                return BadRequest(new { Message = "Invalid employee data" });
            }

            try
            {
                bool isUpdated = _employeeService.UpdateEmployee(employee);
                if (isUpdated)
                {
                    return Ok(new { Message = "Employee updated successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to update employee" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the employee", Error = ex.Message });
            }
        }


        [HttpPost]
        [Route("delete/{id:int}")]
        public EmployeeResponse Delete(int id)
        {
            EmployeeResponse res = new EmployeeResponse();

            try
            {
                

                _employeeService.DeleteEmployees(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _employeeService.GetItems(id);
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
