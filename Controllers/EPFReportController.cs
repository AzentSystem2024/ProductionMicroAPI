using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EPFReportController : ControllerBase
    {
        private readonly IEPFReportService _iEPFReportService;
        public EPFReportController(IEPFReportService iEPFReportService)
        {
            _iEPFReportService = iEPFReportService;
        }
        [HttpPost("epfreport")]
        public EPFReportResponse GetEPFReport(EPFReportRequest request)
        {
            var response = new EPFReportResponse();
            try
            {
                response = _iEPFReportService.GetEPFReport(request);
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
