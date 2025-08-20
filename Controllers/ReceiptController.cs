using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }
        [HttpPost]
        [Route("insert")]
        public ReceiptResponse Insert(Receipt model)
        {
            ReceiptResponse res = new ReceiptResponse();

            try
            {
                res = _receiptService.insert(model);
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
        public ReceiptResponse UpdateReceipt(ReceiptUpdate model)
        {
            ReceiptResponse res = new ReceiptResponse();

            try
            {
                res = _receiptService.Update(model); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("invoicelist")]
        public PendingInvoiceListResponse GetPendingInvoiceList(InvoicependingRequest request)
        {
            PendingInvoiceListResponse res = new PendingInvoiceListResponse();

            try
            {
                res = _receiptService.GetPendingInvoiceList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PendingInvoiceItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public ReceiptListResponse GetReceiptList()
        {
            ReceiptListResponse res = new ReceiptListResponse();

            try
            {
                res = _receiptService.GetReceiptList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<ReceiptListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public ReceiptSelectResponse Select(int id)
        {
            ReceiptSelectResponse response = new ReceiptSelectResponse();
            try
            {
                response = _receiptService.GetReceiptById(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("commit")]
        public ReceiptResponse Commit(ReceiptUpdate model)
        {
            ReceiptResponse response = new ReceiptResponse();
            try
            {
                response = _receiptService.Commit(model);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("receiptno")]
        public RecResponse GetReceiptNo()
        {
            RecResponse res = new RecResponse();

            try
            {
                res = _receiptService.GetReceiptNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public ReceiptResponse Delete(int id)
        {
            ReceiptResponse res = new ReceiptResponse();
            try
            {
                res = _receiptService.Delete(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("ledgerlist")]
        public ReceiptLedgerListResponse GetLedgerList()
        {
            ReceiptLedgerListResponse res = new ReceiptLedgerListResponse();

            try
            {
                res = _receiptService.GetLedgerList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<ReceiptLedgerList>();
            }

            return res;
        }
    }
}
