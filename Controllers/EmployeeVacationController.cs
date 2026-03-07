using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeVacationController : ControllerBase
    {
        private readonly IEmployeeVacationService _employeeVacationService;
        public EmployeeVacationController(IEmployeeVacationService employeeVacationService)
        {
            _employeeVacationService = employeeVacationService;
        }


        [HttpPost]
        [Route("list")]
        public EmployeeVacationLogListResponseData LogList()
        {
            EmployeeVacationLogListResponseData loglist = new EmployeeVacationLogListResponseData();
            try
            {
                loglist = _employeeVacationService.GetAllEmployeeVacation();
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
        public saveEmployeeVacationResponseData SaveData(saveEmployeeVacationData vacationData)
        {
            saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();

            try
            {

                _employeeVacationService.SaveData(vacationData);

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
        public saveEmployeeVacationData selectEmployeeVacationData(int id)
        {
            saveEmployeeVacationData objAdvance = new saveEmployeeVacationData();
            try
            {

                objAdvance = _employeeVacationService.selectEmployeeVacationData(id);
            }
            catch (Exception ex)
            {

            }

            return objAdvance;
        }

        [HttpPost]
        [Route("update")]
        public saveEmployeeVacationResponseData UpdateData(saveEmployeeVacationData vacationData)
        {
            saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();

            try
            {

                _employeeVacationService.UpdateData(vacationData);

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
        public saveEmployeeVacationData Delete(int id)
        {
            saveEmployeeVacationData res = new saveEmployeeVacationData();

            try
            {
                _employeeVacationService.DeleteEmployeeVacation(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        [HttpPost]
        [Route("verify")]
        public saveEmployeeVacationResponseData VerifyData(saveEmployeeVacationData vacationData)
        {
            saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();

            try
            {

                _employeeVacationService.VerifyData(vacationData);

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
        public saveEmployeeVacationResponseData ApproveData(saveEmployeeVacationData vacationData)
        {
            saveEmployeeVacationResponseData res = new saveEmployeeVacationResponseData();

            try
            {
                _employeeVacationService.ApproveData(vacationData);

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
        [Route("emplist")]
        public EmployeeLeaveCreditResponse GetEmployeeLeaveCredit(EmployeeLeaveCreditRequest request)
        {
            EmployeeLeaveCreditResponse loglist = new EmployeeLeaveCreditResponse();
            try
            {
                loglist = _employeeVacationService.GetEmployeeLeaveCredit(request);
            }
            catch (Exception ex)
            {
                loglist.flag = "0";
                loglist.message = ex.Message;
            }
            return loglist;
        }
    }
}
