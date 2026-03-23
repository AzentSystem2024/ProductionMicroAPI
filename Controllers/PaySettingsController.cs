using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaySettingsController : ControllerBase
    {
        private readonly IPaySettingsService _PaySettingsService;
        public PaySettingsController(IPaySettingsService PaySettingsService)
        {
            _PaySettingsService = PaySettingsService;
        }
        [HttpPost("list")]
        public ActionResult<PaySettingsResponce> Get(PaySettingslist request)
        {
            try
            {
                PaySettingsResponce responce = new PaySettingsResponce();
                responce.data = _PaySettingsService.GetPaySettings(request);
                responce.flag = "1";
                responce.message = "success";

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] PaySettings settings)
        {
            try
            {
                PaySettingsResponce responce = new PaySettingsResponce();
                _PaySettingsService.SavePaySettings(settings);
                PaySettingslist request = new PaySettingslist
                {
                    COMPANY_ID = settings.COMPANY_ID
                };

                responce.data = _PaySettingsService.GetPaySettings(request);
                responce.flag = "1";
                responce.message = "Saved successfully";
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return BadRequest(new { flag = 0, message = ex.Message });
            }
        }
    }
}
