using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _DepartmentService;
        public DepartmentController(IDepartmentService DepartmentService)
        {
            _DepartmentService = DepartmentService;
        }
        [HttpPost]
        [Route("list")]
        public DepartmentResponse List(Departmentlist request)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                var data = _DepartmentService.GetAllDepartments(request);

                if (data != null && data.Count > 0)
                {
                    res.flag = "1";
                    res.message = "Success";
                    res.datas = data;
                }
                else
                {
                    res.flag = "0";
                    res.message = "No data found";
                    res.datas = new List<Department>();
                }
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
                res.datas = null;
            }

            return res;
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public DepartmentResponse Select(int id)
        {
            DepartmentResponse responce = new DepartmentResponse();

            try
            {
                var data = _DepartmentService.GetDepartmentById(id);

                if (data != null && data.ID > 0)
                {
                    responce.flag = "1";
                    responce.message = "Success";
                    responce.data = data;
                }
                else
                {
                    responce.flag = "0";
                    responce.message = "No data found";
                    responce.data = null;
                }
            }
            catch (Exception ex)
            {
                responce.flag = "0";
                responce.message = ex.Message;
            }

            return responce;
        }


        [HttpPost]
        [Route("save")]
        public DepartmentResponse SaveData(Department departmentData)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                Int32 ID = _DepartmentService.SaveDepartment(departmentData);

                res.flag = "1";
                res.message = "Success";
                res.data = _DepartmentService.GetDepartmentById(ID);
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
        public DepartmentResponse EditData(Department departmentData)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                Int32 ID = _DepartmentService.SaveDepartment(departmentData);

                res.flag = "1";
                res.message = "Success";
                res.data = _DepartmentService.GetDepartmentById(ID);
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
        public DepartmentResponse Delete(int id)
        {
            DepartmentResponse res = new DepartmentResponse();

            try
            {
                _DepartmentService.DeleteDepartment(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _DepartmentService.GetDepartmentById(id);
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
