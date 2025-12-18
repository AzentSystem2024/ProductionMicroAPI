using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.DataLayer.Services;
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
        public SalesOrderListResponse List(SOListRequest request)
        {
            SalesOrderListResponse response = new SalesOrderListResponse();
            try
            {
                response = _salesOrderService.GetAllSalesOrders(request);
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
        [Route("select/{id:int}")]
        public IActionResult Select(int id)
        {
            try
            {
                SalesOrderDetailSelectResponse response = _salesOrderService.GetSalesOrder(id);
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
                var response = _salesOrderService.EditData(salesOrderUpdate);
                return Ok(response);
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
        [Route("getitem")]
        public ItemListsResponse GetSalesOrderItems()
        {
            ItemListsResponse response = new ItemListsResponse();
            try
            {
                response = _salesOrderService.GetSalesOrderItems();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<ITEMS>();
            }
            return response;
        }

        [HttpPost]
        [Route("gettype")]
        public ItemListsResponse GetarticleType()
        {
            ItemListsResponse response = new ItemListsResponse();
            try
            {
                response = _salesOrderService.GetarticleType();
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<ITEMS>();
            }
            return response;
        }
        [HttpPost]
        [Route("getcategory")]
        public ItemListsResponse Getcategory(SalesOrderRequest request)
        {
            ItemListsResponse response = new ItemListsResponse();
            try
            {
                response = _salesOrderService.Getcategory(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<ITEMS>();
            }
            return response;
        }
        [HttpPost]
        [Route("getartno")]
        public ItemListsResponse GetArtNo(SalesOrderRequest request)
        {
            ItemListsResponse response = new ItemListsResponse();
            try
            {
                response = _salesOrderService.GetArtNo(request);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = ex.Message;
                response.Data = new List<ITEMS>();
            }
            return response;
        }
        [HttpPost]
        [Route("getcolor")]
        public IActionResult GetColor([FromBody] SalesOrderRequest request)
        {
            try
            {
                var response = _salesOrderService.GetColor(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("getpacking")]
        public IActionResult GetPacking([FromBody] SalesOrderRequest request)
        {
            try
            {
                var response = _salesOrderService.GetPacking(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
        [HttpPost]
        [Route("getpair")]
        public IActionResult GetPairQtyByPackingId(PackingPairRequest request)
        {
            try
            {
                var response = _salesOrderService.GetPairQtyByPackingId(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
        [HttpPost]
        [Route("getwarehouse")]
        public IActionResult GetWarehouseByCustId(SOQUOTATIONRequest request)
        {
            try
            {
                var response = _salesOrderService.GetWarehouseByCustId(request);
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
        [HttpPost]
        [Route("listQuotation")]
        public IActionResult ListQuotation([FromBody] SOQUOTATIONRequest request)
        {
            try
            {
                var response = _salesOrderService.GetSOQUOTATIONLIST(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Flag = 0, Message = "Error: " + ex.Message });
            }
        }
        [HttpPost("GetLatestVoucherNumber")]
        public IActionResult GetLatestVoucherNumber()
        {
            try
            {
                var response = _salesOrderService.GetLatestVoucherNumber();
                if (response.Flag == 1)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Flag = 0, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("getsubdealer")]
        public List<Subdealers> GetSubdealer(SubdealerRequest id)
        {
            List<Subdealers> subdealer = new List<Subdealers>();

            try
            {

                subdealer = _salesOrderService.GetSubdealer(id);
            }
            catch (Exception ex)
            {
            }
            return subdealer.ToList();
        }
    }
}
