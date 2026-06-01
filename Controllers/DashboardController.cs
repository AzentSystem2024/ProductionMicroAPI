using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpPost]
        [Route("dashboard")]
        public DashboardResponse GetDashboardSummary([FromBody] DashboardRequest request)
        {
            DashboardResponse response = new DashboardResponse();

            try
            {
                response = _dashboardService.GetDashboardSummary(request);

                response.flag = 1;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}