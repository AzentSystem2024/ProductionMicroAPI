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
        [HttpPost]
        [Route("list")]
        public StockAdjustmentListResponse List()
        {
            StockAdjustmentListResponse response = new StockAdjustmentListResponse();
            try
            {
                response = _stockAdjustmentService.GetAllStockAdjustments();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<StockAdjustmentList>();
            }
            return response;
        }

        [HttpPost]
        [Route("select/{adjId:int}")]
        public IActionResult Select(int adjId)
        {
            try
            {
                StockAdjustmentDetailResponse response = _stockAdjustmentService.GetStockAdjustment(adjId);
                if (response.Flag == 1)
                    return Ok(response);
                else
                    return BadRequest(response.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new StockAdjustmentDetailResponse
                {
                    Flag = 0,
                    Message = ex.Message,
                    Data = new StockAdjustment()
                });
            }
        }

        [HttpPost]
        [Route("save")]
        public StockAdjustmentResponse SaveData(StockAdjustment stockAdjustment)
        {
            StockAdjustmentResponse res = new StockAdjustmentResponse();
            try
            {
                _stockAdjustmentService.SaveData(stockAdjustment);
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
        public IActionResult EditData([FromBody] StockAdjustmentUpdate stockAdjustmentUpdate)
        {
            try
            {
                _stockAdjustmentService.EditData(stockAdjustmentUpdate);
                return Ok(new { Flag = "1", Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Flag = "0", Message = ex.Message });
            }
        }


        [HttpPost]
        [Route("delete/{adjId:int}")]
        public StockAdjustmentResponse Delete(int adjId)
        {
            StockAdjustmentResponse response = new StockAdjustmentResponse();
            try
            {
                bool success = _stockAdjustmentService.DeleteStockAdjustment(adjId);
                if (success)
                {
                    response.Flag = "1";
                    response.Message = "Success";
                }
                else
                {
                    response.Flag = "0";
                    response.Message = "Failed to delete";
                }
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [Route("list-items")]
        public IActionResult ListItems(StockAdjustmentRequest request)
        {
            try
            {
                var response = _stockAdjustmentService.GetStockAdjustmentItems(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
        [HttpPost("Approve")]
        public IActionResult ApproveStockAdjustment([FromBody] StockAdjustmentUpdate stockAdjustment)
        {
            try
            {
                StockAdjustmentResponse response = _stockAdjustmentService.ApproveStockAdjustment(stockAdjustment);
                if (response.Flag == "1")
                    return Ok(response);
                else
                    return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = "0", Message = ex.Message });
            }
        }
    }
}
