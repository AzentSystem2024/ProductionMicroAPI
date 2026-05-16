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
    public class DepartmentGroupController : ControllerBase
    {
        private readonly IDepartmentGroupService _DepartmentGroupService;
        public DepartmentGroupController(IDepartmentGroupService DepartmentService)
        {
            _DepartmentGroupService = DepartmentService;
        }
        [HttpPost]
        [Route("list")]
        public DepartmentGroupResponse List(DepartmentGroupList request)
        {
            DepartmentGroupResponse res = new DepartmentGroupResponse();

            try
            {
                var data = _DepartmentGroupService.GetAllDepartmentGroups(request);

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
                    res.datas = new List<DepartmentGroup>();
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
        public DepartmentGroupResponse Select(int id)
        {
            DepartmentGroupResponse responce = new DepartmentGroupResponse();

            try
            {
                var data = _DepartmentGroupService.GetDepartmentGroupById(id);

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
        public DepartmentGroupResponse SaveData(DepartmentGroup departmentData)
        {
            DepartmentGroupResponse res = new DepartmentGroupResponse();

            try
            {
                Int32 ID = _DepartmentGroupService.SaveDepartmentGroup(departmentData);

                res.flag = "1";
                res.message = "Success";
                res.data = _DepartmentGroupService.GetDepartmentGroupById(ID);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        //[HttpPost]
        //[Route("edit")]
        //public DepartmentResponse EditData(Department departmentData)
        //{
        //    DepartmentResponse res = new DepartmentResponse();

        //    try
        //    {
        //        Int32 ID = _DepartmentGroupService.SaveDepartment(departmentData);

        //        res.flag = "1";
        //        res.message = "Success";
        //        res.data = _DepartmentGroupService.GetDepartmentById(ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = "0";
        //        res.message = ex.Message;
        //    }

        //    return res;
        //}

        [HttpPost]
        [Route("delete/{id:int}")]
        public DepartmentGroupResponse Delete(int id)
        {
            DepartmentGroupResponse res = new DepartmentGroupResponse();

            try
            {
                _DepartmentGroupService.DeleteDepartmentGroup(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _DepartmentGroupService.GetDepartmentGroupById(id);
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
