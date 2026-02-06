using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GetSystemDataController : ControllerBase
    {
        private readonly IGetSystemDataService _getsystemdataService;
        public GetSystemDataController(IGetSystemDataService getsystemdataService)
        {
            _getsystemdataService = getsystemdataService;
        }
        [HttpPost]
        [Route("list")]
        public GetSystemDataResponse GetSystemInfo()
        {

            GetSystemDataResponse res = new GetSystemDataResponse();
            try
            {
                res = _getsystemdataService.GetSystemInfo();

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
