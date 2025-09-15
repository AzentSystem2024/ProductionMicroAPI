using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ESIReportController : ControllerBase
    {
        private readonly IESIReportService _iESIReportService;

        public ESIReportController(IESIReportService iESIReportService)
        {
            _iESIReportService = iESIReportService;
        }
        [HttpPost("esireport")]
        public ESIReportResponse GetESIReport(ESIReportRequest request)
        {
            var response = new ESIReportResponse();
            try
            {
                response = _iESIReportService.GetESIReport(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

