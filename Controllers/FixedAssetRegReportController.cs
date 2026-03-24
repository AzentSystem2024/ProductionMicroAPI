using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedAssetRegReportController : ControllerBase
    {
        private readonly IFixedAssetRegReportService _service;

        public FixedAssetRegReportController(IFixedAssetRegReportService service)
        {
            _service = service;
        }

        [HttpPost("fixedassetreport")]
        public FixedAssetReportResponse GetFixedAssetReport(FixedAssetReportRequest request)
        {
            var response = new FixedAssetReportResponse();

            try
            {
                response = _service.GetFixedAssetReport(request);
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

