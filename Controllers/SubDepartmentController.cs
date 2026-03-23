using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubDepartmentController : ControllerBase
    {
        private readonly ISubDepartmentService _SubDepartmentService;
        public SubDepartmentController(ISubDepartmentService SubDepartmentService)
        {
            _SubDepartmentService = SubDepartmentService;
        }
        [HttpPost]
        [Route("list")]
        public SubDepartmentResponse List()
        {
            SubDepartmentResponse res = new SubDepartmentResponse();

            try
            {
                var data = _SubDepartmentService.GetAllDepartments();

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
                    res.datas = new List<SubDepartment>();
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
        public SubDepartmentResponse Select(int id)
        {
            SubDepartmentResponse responce = new SubDepartmentResponse();

            try
            {
                var data = _SubDepartmentService.GetDepartmentById(id);

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
        public SubDepartmentResponse SaveData(SubDepartment subdepartment)
        {
            SubDepartmentResponse res = new SubDepartmentResponse();

            try
            {
                Int32 ID = _SubDepartmentService.SaveDepartment(subdepartment);

                res.flag = "1";
                res.message = "Success";
                res.data = _SubDepartmentService.GetDepartmentById(ID);
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
        public SubDepartmentResponse EditData(SubDepartment subdepartment)
        {
            SubDepartmentResponse res = new SubDepartmentResponse();

            try
            {
                Int32 ID = _SubDepartmentService.SaveDepartment(subdepartment);

                res.flag = "1";
                res.message = "Success";
                res.data = _SubDepartmentService.GetDepartmentById(ID);
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
        public SubDepartmentResponse Delete(int id)
        {
            SubDepartmentResponse res = new SubDepartmentResponse();

            try
            {
                _SubDepartmentService.DeleteDepartment(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _SubDepartmentService.GetDepartmentById(id);
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
