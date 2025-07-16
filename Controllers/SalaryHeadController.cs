using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Services;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryHeadController : ControllerBase
    {
        private readonly ISalaryHeadService _SalaryHeadService;
        public SalaryHeadController(ISalaryHeadService SalaryHeadService)
        {
            _SalaryHeadService = SalaryHeadService;
        }
        [HttpPost]
        [Route("list")]
        public SalaryHeadListResponse List()
        {
            SalaryHeadListResponse res = new SalaryHeadListResponse();

            try
            {
                res = _SalaryHeadService.GetAllSalaryHead();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = null;
            }

            return res; 
        }


        [HttpPost]
        [Route("select/{id:int}")]
        public SalaryHeadUpdate Select(int id)
        {
            SalaryHeadUpdate salaryHead = new SalaryHeadUpdate();

            try
            {
                salaryHead = _SalaryHeadService.GetItem(id);
            }
            catch (Exception ex)
            {
                // Handle exception
            }

            return salaryHead;
        }

        [HttpPost]
        [Route("save")]
        public SalaryHeadResponse SaveData(SalaryHead salaryHeadData)
        {
            SalaryHeadResponse res = new SalaryHeadResponse();

            try
            {
                Int32 ID = _SalaryHeadService.SaveData(salaryHeadData);

                res.flag = "1";
                res.message = "Success";
                res.data = _SalaryHeadService.GetItem(ID);
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
        public SalaryHeadResponse EditData(SalaryHeadUpdate salaryHeadData)
        {
            SalaryHeadResponse res = new SalaryHeadResponse();

            try
            {
                Int32 ID = _SalaryHeadService.EditData(salaryHeadData);

                res.flag = "1";
                res.message = "Success";
                res.data = _SalaryHeadService.GetItem(ID);
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
        public SalaryHeadResponse Delete(int id)
        {
            SalaryHeadResponse res = new SalaryHeadResponse();

            try
            {
                _SalaryHeadService.DeleteSalaryHead(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _SalaryHeadService.GetItem(id);
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
