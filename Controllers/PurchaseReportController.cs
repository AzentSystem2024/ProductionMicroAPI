using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static MicroApi.Models.PurchaseReport;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseReportController : ControllerBase
    {
        private readonly IPurchaseReportService _reportService;

        public PurchaseReportController(IPurchaseReportService service)
        {
            _reportService = service;
        }
        [HttpPost]
        [Route("PurchaseSummary")]
        public PurchaseSummaryResponse GetPurchaseSummary(PurchaseReport filter)
        {
            var res = new PurchaseSummaryResponse();
            try
            {
                res = _reportService.GetPurchaseSummary(filter);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("itemwisepurchase")]
        public ItemWisePurchaseResponse GetItemWisePurchaseReport(ItemWisePurchaseReportRequest filter)
        {
            var res = new ItemWisePurchaseResponse();
            try
            {
                res = _reportService.GetItemWisePurchaseReport(filter);
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
