using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalStockController : ControllerBase
    {
        private readonly IPhysicalStockService _physicalStockService;
        public PhysicalStockController(IPhysicalStockService physicalStockService)
        {
            _physicalStockService = physicalStockService;
        }
        [HttpPost]
        [Route("list")]
        public PhysicalStockListResponse List()
        {
            PhysicalStockListResponse response = new PhysicalStockListResponse();
            try
            {
                response = _physicalStockService.GetAllPhysicalStocks();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<PhysicalStockList>();
            }
            return response;
        }

        [HttpPost]
        [Route("select/{physicalId:int}")]
        public IActionResult Select(int physicalId)
        {
            try
            {
                PhysicalStockSelectDetailResponse response = _physicalStockService.GetPhysicalStock(physicalId);
                if (response.Flag == 1)
                    return Ok(response);
                else
                    return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PhysicalStockDetailResponse { Flag = 0, Message = ex.Message, Data = new PhysicalStock() });
            }
        }

        [HttpPost]
        [Route("save")]
        public PhysicalStockResponse SaveData(PhysicalStock physicalStock)
        {
            PhysicalStockResponse res = new PhysicalStockResponse();
            try
            {
                _physicalStockService.SaveData(physicalStock);
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
        public IActionResult EditData([FromBody] PhysicalStockUpdate physicalStockUpdate)
        {
            try
            {
                _physicalStockService.EditData(physicalStockUpdate);
                return Ok(new { Flag = "1", Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Flag = "0", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("delete/{physicalId:int}")]
        public PhysicalStockResponse Delete(int physicalId)
        {
            PhysicalStockResponse response = new PhysicalStockResponse();
            try
            {
                bool success = _physicalStockService.DeletePhysicalStock(physicalId);
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
        public IActionResult ListItems(PhysicalStockRequest request)
        {
            try
            {
                var response = _physicalStockService.GetPhysicalStockItems(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }

        [HttpPost("Approve")]
        public IActionResult ApprovePhysicalStock([FromBody] PhysicalStockApproval physicalStock)
        {
            try
            {
                PhysicalStockResponse response = _physicalStockService.ApprovePhysicalStock(physicalStock);
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
        [HttpPost]
        [Route("filtered-items")]
        public IActionResult GetFilteredItems([FromBody] FilteredItemsRequest request)
        {
            try
            {
                List<StockItems> items = _physicalStockService.GetFilteredItems(request);
                return Ok(new { Flag = 1, Message = "Success", Data = items });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
        [HttpPost]
        [Route("list-Barcodes")]
        public IActionResult GetPhysicalStockItemsByItemCodes([FromBody] ItemCodeRequest request)
        {
            try
            {
                var response = _physicalStockService.GetPhysicalStockItemsByItemCodes(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("history")]
        public IActionResult GetPhysicalStockHistory()
        {
            try
            {
                var history = _physicalStockService.GetPhysicalStockHistory();
                return Ok(new { Flag = 1, Message = "Success", Data = history });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
    }
}

