using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Sales_InvoiceController : ControllerBase
    {
        private readonly ISales_InvoiceService _salesinvoiceService;
        public Sales_InvoiceController(ISales_InvoiceService salesinvoiceService)
        {
            _salesinvoiceService = salesinvoiceService;
        }
        [HttpPost]
        [Route("insert")]
        public SalesInvoiceResponse Insert(Sales_Invoice model)
        {
            SalesInvoiceResponse res = new SalesInvoiceResponse();

            try
            {
                res = _salesinvoiceService.insert(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("update")]
        public SalesInvoiceResponse Update(Sale_InvoiceUpdate model)
        {
            SalesInvoiceResponse res = new SalesInvoiceResponse();
            try
            {
                res = _salesinvoiceService.Update(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("list")]
        public DeliveryGridResponse GetCreditNoteList(DeliveryInvoiceRequest request)
        {
            DeliveryGridResponse res = new DeliveryGridResponse();

            try
            {
                res = _salesinvoiceService.GetTransferData(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<DeliveryGridItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("getlist")]
        public SalesInvoiceHeaderResponse GetSaleInvoiceHeaderData()
        {
            SalesInvoiceHeaderResponse res = new SalesInvoiceHeaderResponse();

            try
            {
                res = _salesinvoiceService.GetSaleInvoiceHeaderData();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SalesInvoiceHeader>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public SalesInvselectResponse Select(int id)
        {
            SalesInvselectResponse response = new SalesInvselectResponse();
            try
            {
                response = _salesinvoiceService.GetSaleInvoiceById(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public SalesInvoicesaveResponse Delete(int id)
        {
            SalesInvoicesaveResponse res = new SalesInvoicesaveResponse();
            try
            {
                _salesinvoiceService.Delete(id);
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("approve")]
        public SalesInvoiceResponse Approve(Sale_InvoiceUpdate model)
        {
            SalesInvoiceResponse res = new SalesInvoiceResponse();
            try
            {
                res = _salesinvoiceService.Approve(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }
            return res;
        }
        [HttpPost("GetLatestVoucherNumber")]
        public IActionResult GetLatestVoucherNumber()
        {
            try
            {
                var response = _salesinvoiceService.GetLatestVoucherNumber();
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
    }
}
