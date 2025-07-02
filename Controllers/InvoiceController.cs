using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpPost]
        [Route("insert")]
        public InvoiceResponse Insert(Invoice model)
        {
            InvoiceResponse res = new InvoiceResponse();

            try
            {
                res = _invoiceService.insert(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public TransferGridResponse GetCreditNoteList()
        {
            TransferGridResponse res = new TransferGridResponse();

            try
            {
                res = _invoiceService.GetTransferData();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<TransferGridItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("getlist")]
        public InvoiceHeaderResponse GetSaleInvoiceHeaderData()
        {
            InvoiceHeaderResponse res = new InvoiceHeaderResponse();

            try
            {
                res = _invoiceService.GetSaleInvoiceHeaderData();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<InvoiceHeader>();
            }

            return res;
        }
    }
}
