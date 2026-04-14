using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly ISalesInvoiceService _salesinvoiceService;

        public SalesInvoiceController(ISalesInvoiceService salesinvoiceService)
        {
            _salesinvoiceService = salesinvoiceService;
        }
        [HttpPost]
        [Route("getitem")]
        public SalesResponse GetSalesInvoiceItem(SalesInvoiceRequest request)
        {
            SalesResponse res = new SalesResponse();

            try
            {
                res = _salesinvoiceService.GetSalesInvoiceItem(request);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SalesInvoice>();
            }

            return res;
        }
        [HttpPost]
        [Route("insert")]
        public SalesInvoiceInsertResponse Insert(SalesInvoiceInsertRequest input)
        {
            SalesInvoiceInsertResponse res = new SalesInvoiceInsertResponse();

            try
            {
                res = _salesinvoiceService.Insert(input);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("update")]
        public SalesInvoiceInsertResponse Update(SalesInvoiceInsertRequest input)
        {
            SalesInvoiceInsertResponse res = new SalesInvoiceInsertResponse();

            try
            {
                res = _salesinvoiceService.Update(input);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("commit")]
        public SalesInvoiceInsertResponse Commit(SalesInvoiceInsertRequest input)
        {
            SalesInvoiceInsertResponse res = new SalesInvoiceInsertResponse();

            try
            {
                res = _salesinvoiceService.Commit(input);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public SalesInvoiceListResponse GetSalesInvoiceHeaderList(InvoiceListRequest request)
        {
            SalesInvoiceListResponse res = new SalesInvoiceListResponse();

            try
            {
                res = _salesinvoiceService.GetSalesInvoiceHeaderList(request);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SalesInvoiceListHeader>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public SalesInvoiceViewResponse GetSalesInvoiceById(int id)
        {
            SalesInvoiceViewResponse response = new SalesInvoiceViewResponse();
            try
            {
                response = _salesinvoiceService.GetSalesInvoiceById(id);
            }
            catch (Exception ex)
            {
                response.Flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public SalesInvoiceInsertResponse Delete(int id)
        {
            SalesInvoiceInsertResponse res = new SalesInvoiceInsertResponse();
            try
            {
                res = _salesinvoiceService.Delete(id);
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
    }
}
