using MicroApi.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using MicroApi.Models;
//using RetailApi.DAL.Services;
using MicroApi.DataLayer.Service;

namespace MicroApi.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly IHospitalService _hospitalService;
        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpPost]
        [Route("insert")]
        public HospitalResponse Insert(Hospital hospital)
        {
            HospitalResponse res = new HospitalResponse();
            try
            {
                res =_hospitalService.Insert(hospital);

            }
            catch(Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("update")]

        public HospitalResponse Update(HospitalUpdate hospital)
        {
            HospitalResponse res = new HospitalResponse();
            try
            {
               res = _hospitalService.Update(hospital);
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
        public HospitalResponse select(int id)
        {
            HospitalResponse res = new HospitalResponse();
            try
            {

                res = _hospitalService.GetHospitalById(id);
             
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
        public HospitalListResponse HospitalLogList()
        {

            HospitalListResponse res = new HospitalListResponse();
            try
            {
                res = _hospitalService.GetLogList();
                
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
        public HospitalResponse Delete(int id)
        {
            HospitalResponse res = new HospitalResponse();
            try
            {
               res= _hospitalService.DeleteHospitalData(id);
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
