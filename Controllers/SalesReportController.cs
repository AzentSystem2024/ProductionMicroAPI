using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesReportController : ControllerBase
    {
        private readonly ISalesReportService _service;

        public SalesReportController(ISalesReportService service)
        {
            _service = service;
        }

        [HttpPost("SalesSummary")]
        public IActionResult GetSalesSummary([FromBody] SalesSummaryFilter request)
        {
            return Ok(_service.GetSalesSummary(request));
        }

        [HttpPost("SalesDetail")]
        public IActionResult GetSalesDetail([FromBody] SalesDetailFilter request)
        {
            return Ok(_service.GetSalesDetails(request));
        }

        [HttpPost("ConsignmentSummary")]
        public IActionResult GetConsignmentSummary([FromBody] ConsignmentSummaryFilter request)
        {
            return Ok(_service.GetConsignmentSummary(request));
        }

        [HttpPost("ConsignmentReturnDetail")]
        public IActionResult GetConsignmentReturnDetail([FromBody] ConsignmentReturnDetailFilter request)
        {
            return Ok(_service.GetConsignmentReturnDetail(request));
        }
    }
}