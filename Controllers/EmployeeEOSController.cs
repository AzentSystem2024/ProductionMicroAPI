using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeEOSController : ControllerBase
    {
        private readonly IEmployeeEOSService _employeeEOSService;
        public EmployeeEOSController(IEmployeeEOSService employeeEOSService)
        {
            _employeeEOSService = employeeEOSService;
        }


        [HttpPost]
        [Route("list")]
        public EmployeeEOSLogListResponseData LogList()
        {
            EmployeeEOSLogListResponseData loglist = new EmployeeEOSLogListResponseData();
            try
            {
                loglist = _employeeEOSService.GetAllEmployeeEOS();
            }
            catch (Exception ex)
            {
                loglist.flag = "0";
                loglist.message = ex.Message;
            }
            return loglist;
        }

        [HttpPost]
        [Route("save")]
        public saveEmployeeEOSResponseData SaveData(saveEmployeeEOSData eosData)
        {
            saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();

            try
            {

                _employeeEOSService.SaveData(eosData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }


        [HttpPost]
        [Route("select/{id:int}")]
        public saveEmployeeEOSData selectEmployeeEOSData(int id)
        {
            saveEmployeeEOSData objEos = new saveEmployeeEOSData();
            try
            {

                objEos = _employeeEOSService.selectEmployeeEOSData(id);
            }
            catch (Exception ex)
            {

            }

            return objEos;
        }

        [HttpPost]
        [Route("update")]
        public saveEmployeeEOSResponseData UpdateData(saveEmployeeEOSData eosData)
        {
            saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();

            try
            {

                _employeeEOSService.UpdateData(eosData);

                res.flag = "1";
                res.message = "Success";

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
        public saveEmployeeEOSResponseData Delete(int id)
        {
            saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();

            try
            {

                _employeeEOSService.DeleteEmployeeEOS(id);
                res.flag = "1";
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("verify")]
        public saveEmployeeEOSResponseData VerifyData(saveEmployeeEOSData eosData)
        {
            saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();

            try
            {

                _employeeEOSService.VerifyData(eosData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("approve")]
        public saveEmployeeEOSResponseData ApproveData(saveEmployeeEOSData eosData)
        {
            saveEmployeeEOSResponseData res = new saveEmployeeEOSResponseData();

            try
            {

                _employeeEOSService.ApproveData(eosData);

                res.flag = "1";
                res.message = "Success";

            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("getEmployeeData")]
        public EOSEmployeeData getEOSEmployeeData(EOSEmployeeInput inp)
        {
            EOSEmployeeData objEmp = new EOSEmployeeData();
            try
            {

                objEmp = _employeeEOSService.getEOSEmployeeData(inp);
            }
            catch (Exception ex)
            {

            }

            return objEmp;
        }
    }
}
