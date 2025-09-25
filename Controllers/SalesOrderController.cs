using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;

        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }
        [HttpPost]
        [Route("list")]
        public SalesOrderListResponse List()
        {
            SalesOrderListResponse response = new SalesOrderListResponse();
            try
            {
                response = _salesOrderService.GetAllSalesOrders();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<SalesOrderList>();
            }
            return response;
        }

        [HttpPost]
        [Route("select/{soId:int}")]
        public IActionResult Select(int soId)
        {
            try
            {
                SalesOrderDetailSelectResponse response = _salesOrderService.GetSalesOrder(soId);
                if (response.Flag == 1)
                    return Ok(response);
                else
                    return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new SalesOrderDetailSelectResponse
                {
                    Flag = 0,
                    Message = ex.Message,
                    Data = new SalesOrderSelect { Details = new List<SalesOrderDetailSelect>() }
                });
            }
        }

        [HttpPost]
        [Route("save")]
        public SalesOrderResponse SaveData([FromBody] SalesOrder salesOrder)
        {
            SalesOrderResponse res = new SalesOrderResponse();
            try
            {
                _salesOrderService.SaveData(salesOrder);
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
        public IActionResult EditData([FromBody] SalesOrderUpdate salesOrderUpdate)
        {
            try
            {
                _salesOrderService.EditData(salesOrderUpdate);
                return Ok(new { Flag = "1", Message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Flag = "0", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("delete/{soId:int}")]
        public SalesOrderResponse Delete(int soId)
        {
            SalesOrderResponse response = new SalesOrderResponse();
            try
            {
                bool success = _salesOrderService.DeleteSalesOrder(soId);
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
        public IActionResult ListItems([FromBody] SalesOrderRequest request)
        {
            try
            {
                var response = _salesOrderService.GetSalesOrderItems(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }

        [HttpPost("approve")]
        public IActionResult ApproveSalesOrder([FromBody] SalesOrderUpdate request)
        {
            var response = _salesOrderService.ApproveSalesOrder(request);
            if (response.Flag == "1")
                return Ok(response);
            else
                return BadRequest(response);
        }

    }
}
