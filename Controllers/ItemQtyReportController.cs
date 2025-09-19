using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemQtyReportController : ControllerBase
    {
        private readonly IItemQtyReportService _itemQtyReportService;
        public ItemQtyReportController(IItemQtyReportService itemQtyReportService)
        {
            _itemQtyReportService = itemQtyReportService;
        }
        [HttpPost("itemquantityreport")]
        public ItemQuantityReportResponse GetItemQuantityReport()
        {
            var response = new ItemQuantityReportResponse();
            try
            {
                response = _itemQtyReportService.GetItemQuantityReport();
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
