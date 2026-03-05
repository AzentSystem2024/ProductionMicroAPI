using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveTypeService;
        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }


        [HttpPost]
        [Route("list")]
        public LeaveTypeLogListResponseData LogList()
        {
            LeaveTypeLogListResponseData loglist = new LeaveTypeLogListResponseData();
            try
            {
                loglist = _leaveTypeService.GetAllLeaveType();
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
        public saveLeaveTypeResponseData SaveData(saveLeaveTypeData typeData)
        {
            saveLeaveTypeResponseData res = new saveLeaveTypeResponseData();

            try
            {

                _leaveTypeService.SaveData(typeData);

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
        public saveLeaveTypeData selectLeaveTypeData(int id)
        {
            saveLeaveTypeData objType = new saveLeaveTypeData();
            try
            {

                objType = _leaveTypeService.selectLeaveTypeData(id);
            }
            catch (Exception ex)
            {

            }

            return objType;
        }

        [HttpPost]
        [Route("update")]
        public saveLeaveTypeResponseData UpdateData(saveLeaveTypeData typeData)
        {
            saveLeaveTypeResponseData res = new saveLeaveTypeResponseData();

            try
            {

                _leaveTypeService.UpdateData(typeData);

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
        public saveLeaveTypeResponseData Delete(int id)
        {
            saveLeaveTypeResponseData res = new saveLeaveTypeResponseData();

            try
            {

                _leaveTypeService.DeleteLeaveType(id);
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


    }
}
