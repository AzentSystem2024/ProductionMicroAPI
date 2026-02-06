using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
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
        public ItemQuantityReportResponse GetItemQuantityReport(ItemQuantityReportRequest request)
        {
            var response = new ItemQuantityReportResponse();
            try
            {
                response = _itemQtyReportService.GetItemQuantityReport(request);
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
