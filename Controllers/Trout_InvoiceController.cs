using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.DataLayer.Services;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Trout_InvoiceController : ControllerBase
    {
        private readonly ITrout_InvoiceService _trout_InvoiceService;

        public Trout_InvoiceController(ITrout_InvoiceService trout_InvoiceService)
        {
            _trout_InvoiceService = trout_InvoiceService;
        }
        [HttpPost]
        [Route("insert")]
        public Trout_InvoiceResponse Insert(Trout_Invoice model)
        {
            Trout_InvoiceResponse res = new Trout_InvoiceResponse();

            try
            {
                res = _trout_InvoiceService.insert(model);
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
        public Trout_InvoiceResponse update(Trout_InvoiceUpdate model)
        {
            Trout_InvoiceResponse res = new Trout_InvoiceResponse();

            try
            {
                res = _trout_InvoiceService.update(model);
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
        public PendingDeliverydataResponse GetTransferData(PendingDeliverydataRequest request)
        {
            PendingDeliverydataResponse res = new PendingDeliverydataResponse();

            try
            {
                res = _trout_InvoiceService.GetTransferData(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PendingDeliverydata>();
            }

            return res;
        }
        [HttpPost]
        [Route("getlist")]
        public Trout_InvoiceListResponse GetSaleInvoiceHeaderData()
        {
            Trout_InvoiceListResponse res = new Trout_InvoiceListResponse();

            try
            {
                res = _trout_InvoiceService.GetSaleInvoiceHeaderData();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<Trout_InvoiceList>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public Trout_InvoiceSelectResponse Select(int id)
        {
            Trout_InvoiceSelectResponse response = new Trout_InvoiceSelectResponse();
            try
            {
                response = _trout_InvoiceService.GetSaleInvoiceById(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("approve")]
        public Trout_InvoiceResponse commit(Trout_InvoiceUpdate model)
        {
            Trout_InvoiceResponse res = new Trout_InvoiceResponse();

            try
            {
                res = _trout_InvoiceService.commit(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("invoiceno")]
        public TroutInvResponse GetInvoiceNo()
        {
            TroutInvResponse res = new TroutInvResponse();

            try
            {
                res = _trout_InvoiceService.GetInvoiceNo();
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
        public Trout_InvoiceResponse Delete(int id)
        {
            Trout_InvoiceResponse res = new Trout_InvoiceResponse();
            try
            {
                res = _trout_InvoiceService.Delete(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("cust")]
        public List<TroutCust_stateName> Getcustlist()
        {
            List<TroutCust_stateName> customers = new List<TroutCust_stateName>();

            try
            {

                customers = _trout_InvoiceService.Getcustlist();
            }
            catch (Exception ex)
            {
            }
            return customers.ToList();
        }
    }
}
