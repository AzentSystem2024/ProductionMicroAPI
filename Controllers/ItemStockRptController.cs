using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemStockRptController : ControllerBase
    {
        private readonly IItemStockRptService _itemStockRptService;

        public ItemStockRptController(IItemStockRptService itemStockRptService)
        {
            _itemStockRptService = itemStockRptService;
        }
        [HttpPost]
        [Route("itemstockrpt")]
        public ItemStockViewResponse GetItemStockView(ItemStockRptRequest request)
        {
            ItemStockViewResponse res = new ItemStockViewResponse();

            try
            {
                res = _itemStockRptService.GetItemStockView(request);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
