using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustTypeDropController: ControllerBase
    {
        private readonly ICustTypeDropService _CustTypeDropService;
        public CustTypeDropController(ICustTypeDropService custTypeDropService)
        {
            _CustTypeDropService = custTypeDropService;
        }
        [HttpPost]
        public IActionResult GetList()
        {
            List<CustTypeDrop> vData = new List<CustTypeDrop>();
            try
            {
                vData = _CustTypeDropService.GetCustomerTypeDropdown();
            }
            catch (System.Exception ex)
            {
                // Log exception here if needed
                return StatusCode(500, "Internal server error");
            }
            return Ok(vData);
        }

    }
}
