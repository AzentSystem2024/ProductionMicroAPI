using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockAdjustmentController : ControllerBase
    {
        private readonly IStockAdjustmentService _stockAdjustmentService;
        public StockAdjustmentController(IStockAdjustmentService stockAdjustmentService)
        {
            _stockAdjustmentService = stockAdjustmentService;
        }
        //[HttpPost]
        //[Route("list")]
        //public IActionResult List([FromBody] StockAdjustmentRequest request)
        //{
        //    try
        //    {
        //        var response = _stockAdjustmentService.GetAllStockAdjustments(request.STORE_ID, request.COMPANY_ID);
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("select")]
        //public StockAdjustmentListResponse Select([FromBody] StockAdjustmentSelectRequest request)
        //{
        //    StockAdjustmentListResponse response = new StockAdjustmentListResponse();
        //    try
        //    {
        //        response = _stockAdjustmentService.GetStockAdjustment(request.ADJ_ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //    }
        //    return response;
        //}

        [HttpPost]
        [Route("save")]
        public StockAdjustmentResponse SaveData(StockAdjustment stockAdjustment)
        {
            StockAdjustmentResponse res = new StockAdjustmentResponse();
            try
            {
                int ID = _stockAdjustmentService.SaveData(stockAdjustment);
                res.Flag = "1";
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = "0";
                res.Message = ex.Message;
            }
            return res;
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult EditData([FromBody] StockAdjustment stockAdjustmentUpdate)
        {
            try
            {
                int ID = _stockAdjustmentService.EditData(stockAdjustmentUpdate);
                return Ok(new { Flag = "1", Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Flag = "0", Message = ex.Message });
            }
        }

        //[HttpPost]
        //[Route("delete")]
        //public StockAdjustmentListResponse Delete(int adjId)
        //{
        //    StockAdjustmentListResponse res = new StockAdjustmentListResponse();
        //    try
        //    {
        //        _stockAdjustmentService.DeleteStockAdjustment(adjId);
        //        res.Flag = 1;
        //        res.Message = "Deleted successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Flag = 0;
        //        res.Message = "Error: " + ex.Message;
        //    }
        //    return res;
        //}

        [HttpPost]
        [Route("list-items")]
        public IActionResult ListItems([FromBody] StockAdjustmentRequest request)
        {
            try
            {
                var response = _stockAdjustmentService.GetStockAdjustmentItems(request.STORE_ID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
    }
}
