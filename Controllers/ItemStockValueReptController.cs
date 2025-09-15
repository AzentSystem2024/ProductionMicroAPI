using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemStockValueReptController : ControllerBase
    {
        private readonly IItemStockValueReptService _itemStockValueReportService;
        public ItemStockValueReptController(IItemStockValueReptService itemStockValueReportService)
        {
            _itemStockValueReportService = itemStockValueReportService;
        }
        [HttpPost("itemstockvaluereport")]
        public ItemStockValueReportResponse GetItemStockValueReport(ItemStockValueReportRequest request)
        {
            var response = new ItemStockValueReportResponse();
            try
            {
                response = _itemStockValueReportService.GetItemStockValueReport(request);
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
