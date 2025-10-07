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
    }
}
