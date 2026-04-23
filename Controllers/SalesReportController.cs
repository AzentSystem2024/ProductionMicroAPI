using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        //[HttpPost("SalesSummary")]
        //public IActionResult GetSalesSummary([FromBody] SalesSummaryFilter request)
        //{
        //    return Ok(_service.GetSalesSummary(request));
        //}
        [HttpPost("SalesSummary")]
        public IActionResult GetSalesSummary([FromBody] SalesSummaryFilter filter)
        {
            try
            {
                var dt = _service.GetSalesSummary(filter);

                var result = dt.AsEnumerable()
                    .Select(row => dt.Columns
                        .Cast<DataColumn>()
                        .ToDictionary(
                            col => col.ColumnName,
                            col => row[col] == DBNull.Value ? null : row[col]
                        ))
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
        [HttpPost("ItemWiseSales")]
        public IActionResult GetItemWiseSales([FromBody] ItemWiseSalesFilter request)
        {
            return Ok(_service.GetItemWiseSales(request));
        }
        [HttpPost("ItemWiseSalesSummary")]
        public IActionResult GetItemWiseSalesSummary([FromBody] ItemWiseSalesSummaryFilter request)
        {
            return Ok(_service.GetItemWiseSalesSummary(request));
        }
        [HttpPost("DiscountWiseSales")]
        public IActionResult GetDiscountWiseSales([FromBody] DiscountWiseSalesFilter request)
        {
            return Ok(_service.GetDiscountWiseSales(request));
        }
        [HttpPost("TenderReport")]
        public IActionResult GetTenderReport([FromBody] TenderReportFilter request)
        {
            return Ok(_service.GetTenderReport(request));
        }
        [HttpPost("TenderSummary")]
        public IActionResult GetTenderSummary([FromBody] TenderSummaryFilter request)
        {
            return Ok(_service.GetTenderSummary(request));
        }

    }
}