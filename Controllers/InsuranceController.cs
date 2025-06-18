using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        public InsuranceController(InsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }
        [HttpPost]
        [Route("insert")]
        public InsuranceResponse Insert(Insurance insurance)
        {
            InsuranceResponse res = new InsuranceResponse();
            try
            {
               res = _insuranceService.Insert(insurance);
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
        public InsuranceResponse Update(InsuranceUpdate insurance)
        {
            InsuranceResponse res = new InsuranceResponse();
            try
            {
               res =  _insuranceService.Update(insurance);
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
        public InsuranceResponse select(int id)
        {
            InsuranceResponse res = new InsuranceResponse();
            try
            {
                res = _insuranceService.GetInsuranceById(id);
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
        public InsuranceListResponse InsLogList()
        {

            InsuranceListResponse res = new InsuranceListResponse();
            try
            {
                res = _insuranceService.GetLogList();

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
        public InsuranceResponse Delete(int id)
        {
            InsuranceResponse res = new InsuranceResponse();
            try
            {
               res =  _insuranceService.DeleteInsuranceData(id);
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
