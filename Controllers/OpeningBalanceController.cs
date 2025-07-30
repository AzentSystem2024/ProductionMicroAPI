using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpeningBalanceController : ControllerBase
    {
        private readonly IOpeningBalanceService _OpeningService;
        public OpeningBalanceController(IOpeningBalanceService openingService)
        {
            _OpeningService = openingService;
        }
        [HttpPost]
        [Route("select")]
        public OBResponse GetopeningBalance(OBRequest request)
        {
            OBResponse res = new OBResponse();

            try
            {
                res = _OpeningService.GetopeningBalance(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        [HttpPost]
        [Route("insert")]
        public OBResponse Insert(AcOpeningBalanceInsertRequest request)
        {
            OBResponse res = new OBResponse();
                try
                {
                    res = _OpeningService.Insert(request);
                }
                catch (Exception ex)
                {
                    res.flag = 0;
                    res.Message = "Error: " + ex.Message;
                }

                return res;
        }


        }
    }
