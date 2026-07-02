using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }
        [HttpPost]
        [Route("insert")]
        public DistrictResponse Insert(District district)
        {
            DistrictResponse res = new DistrictResponse();
            try
            {
                res = _districtService.Insert(district);
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
        public DistrictResponse Update(District district)
        {
            DistrictResponse res = new DistrictResponse();
            try
            {
                res = _districtService.Update(district);
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
        public DistrictListResponse GetDistrictList()
        {
            DistrictListResponse res = new DistrictListResponse();
            try
            {
                res = _districtService.GetDistrictList();
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
        public DistrictResponse GetDistrictById(int id)
        {
            DistrictResponse res = new DistrictResponse();

            try
            {
                res = _districtService.GetDistrictById(id);
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
        public DistrictResponse Delete(int id)
        {
            DistrictResponse res = new DistrictResponse();
            try
            {
                res = _districtService.Delete(id);
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
