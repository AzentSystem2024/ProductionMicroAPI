using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ACDefaultsController : ControllerBase
    {
        private readonly IACDefaultsService _ACDefaultsService;
        public ACDefaultsController(IACDefaultsService ACDefaultsService)
        {
            _ACDefaultsService = ACDefaultsService;
        }
        [HttpPost]
        [Route("list")]
        public AcDefaultsListResponse GetACDefaultsList(AcDefaultsListReq request)
        {
            var res = new AcDefaultsListResponse();
            try
            {
                res = _ACDefaultsService.GetACDefaultsList(request);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("insert")]
        public AcDefaultsListResponse Save(ACDefaults request)
        {
            var res = new AcDefaultsListResponse();
            try
            {
                res = _ACDefaultsService.Save(request);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
    }
}
