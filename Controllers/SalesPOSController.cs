using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesPOSController : ControllerBase
    {
        private readonly ISalesPOSService _salesposService;

        public SalesPOSController(ISalesPOSService salesposService)
        {
            _salesposService = salesposService;
        }

        [HttpPost("select/{id:int}")]
        public IActionResult GetSalesPOSById(int id)
        {
            var response = new
            {
                flag = "0",
                message = "",
                data = (SalesPOS)null
            };

            try
            {
                var data = _salesposService.GetSalesPOSById(id);

                if (data != null && data.Header != null)
                {
                    return Ok(new
                    {
                        flag = "1",
                        message = "Success",
                        data = data
                    });
                }
                else
                {
                    return Ok(new
                    {
                        flag = "0",
                        message = "No data found",
                        data = (SalesPOS)null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    flag = "0",
                    message = ex.Message,
                    data = (SalesPOS)null
                });
            }
        }
    }
}
