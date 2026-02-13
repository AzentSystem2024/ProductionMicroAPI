using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockMovementRptController : ControllerBase
    {
        private readonly IStockMovementRptService _stockMovementRptService;
        public StockMovementRptController(IStockMovementRptService stockMovementRptService)
        {
            _stockMovementRptService = stockMovementRptService;
        }
        [HttpPost]
        [Route("stockrpt")]
        public StockMovementRptResponse GetStockMovementReport(StockMovementRequest request)
        {
            var res = new StockMovementRptResponse();
            try
            {
                res = _stockMovementRptService.GetStockMovementReport(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("stockDrilldown")]
        public StockMovementDrilldownResponse GetStockMovementDrilldown(StockMovementDrillDownRequest request)
        {
            var res = new StockMovementDrilldownResponse();
            try
            {
                res = _stockMovementRptService.GetStockMovementDrilldown(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
    }
}
