using Microsoft.AspNetCore.Mvc;
using MicroApi.Helper;
using MicroApi.Models;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Test response = new Test();

            try
            {
                using (SqlConnection conn = ADO.GetConnection())
                {
                    response.Flag = 1;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
