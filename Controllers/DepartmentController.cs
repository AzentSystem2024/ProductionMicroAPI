using MicroApi.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using MicroApi.Models;
using RetailApi.DAL.Services;
using MicroApi.DataLayer.Service;
namespace MicroApi.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpPost]
        [Route("insert")]
        public DepartmentResponse Insert(Department department)
        {
            DepartmentResponse res = new DepartmentResponse();
            try
            {
               res= _departmentService.Insert(department);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]

        public DepartmentResponse Update(DepartmentUpdate department)
        {
            DepartmentResponse res = new DepartmentResponse();
            try
            {
               res= _departmentService.Update(department);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public DepartmentResponse select(int id)
        {
            DepartmentResponse res = new DepartmentResponse();
            try
            {

                res = _departmentService.GetDepartmentById(id);

            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;

            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public DepartmentListResponse DeptLogList()
        {

            DepartmentListResponse res = new DepartmentListResponse();
            try
            {
                res = _departmentService.GetLogList();
               
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public DepartmentResponse Delete(int id)
        {
            DepartmentResponse res = new DepartmentResponse();
            try
            {
               res= _departmentService.DeleteDepartmentData(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

    }

}
