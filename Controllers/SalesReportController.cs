using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Net.WebRequestMethods;

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
                        )).ToList();

                return Ok(result);   // ✅ FIXED
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SalesDetail")]
        public IActionResult GetSalesDetail([FromBody] SalesDetailFilter request)
        {
            //return Ok(_service.GetSalesDetails(request));
            try
            {
                var dt = _service.GetSalesDetails(request);

                var result = dt.AsEnumerable()
                    .Select(row => dt.Columns
                        .Cast<DataColumn>()
                        .ToDictionary(
                            col => col.ColumnName,
                            col => row[col] == DBNull.Value ? null : row[col]
                        )).ToList();

                return Ok(result);   // ✅ FIXED
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
            try
            {
                var dt = _service.GetItemWiseSalesSummary(request);

                var result = dt.AsEnumerable()
                    .Select(row => dt.Columns
                        .Cast<DataColumn>()
                        .ToDictionary(
                            col => col.ColumnName,
                            col => row[col] == DBNull.Value ? null : row[col]
                        )).ToList();

                return Ok(result);   // ✅ FIXED
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DiscountWiseSales")]

        public IActionResult GetDiscountWiseSales([FromBody] DiscountWiseSalesFilter request)
        {
            try
            {
                var dt = _service.GetDiscountWiseSales(request);

                var result = dt.AsEnumerable()
                    .Select(row => dt.Columns
                        .Cast<DataColumn>()
                        .ToDictionary(
                            col => col.ColumnName,
                            col => row[col] == DBNull.Value ? null : row[col]
                        )).ToList();

                return Ok(result);   // ✅ FIXED
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TenderReport")]
        public IActionResult GetTenderReport([FromBody] TenderReportFilter request)
        {
            try
            {
                var dt = _service.GetTenderReport(request);

                var result = dt.AsEnumerable()
                    .Select(row => dt.Columns
                        .Cast<DataColumn>()
                        .ToDictionary(
                            col => col.ColumnName,
                            col => row[col] == DBNull.Value ? null : row[col]
                        )).ToList();

                return Ok(result);   // ✅ FIXED
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TenderSummary")]
        public IActionResult GetTenderSummary([FromBody] TenderSummaryFilter request)
        {
            try
            {
                var dt = _service.GetTenderSummary(request);

                var result = dt.AsEnumerable()
                    .Select(row => dt.Columns
                        .Cast<DataColumn>()
                        .ToDictionary(
                            col => col.ColumnName,
                            col => row[col] == DBNull.Value ? null : row[col]
                        )).ToList();

                return Ok(result);   // ✅ FIXED
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}