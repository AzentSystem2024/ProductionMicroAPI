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
        [Route("update")]
        public InvoiceResponse Update(InvoiceUpdate model)
        {
            InvoiceResponse res = new InvoiceResponse();
            try
            {
                res = _invoiceService.Update(model);
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
        public TransferGridResponse GetCreditNoteList(TransferInvoiceRequest request)
        {
            TransferGridResponse res = new TransferGridResponse();

            try
            {
                res = _invoiceService.GetTransferData(request);
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
        [HttpPost]
        [Route("select/{id:int}")]
        public InvoiceHeaderSelectResponse Select(int id)
        {
            InvoiceHeaderSelectResponse response = new InvoiceHeaderSelectResponse();
            try
            {
                response = _invoiceService.GetSaleInvoiceById(id);
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
        public InvoiceResponse Commit(CommitInvoiceRequest request)
        {
            InvoiceResponse response = new InvoiceResponse();
            try
            {
                response = _invoiceService.CommitInvoice(request);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("invoiceno")]
        public InvResponse GetInvoiceNo()
        {
            InvResponse res = new InvResponse();

            try
            {
                res = _invoiceService.GetInvoiceNo();
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
        public InvoiceResponse Delete(int id)
        {
            InvoiceResponse res = new InvoiceResponse();
            try
            {
                res = _invoiceService.Delete(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }

    }
}
