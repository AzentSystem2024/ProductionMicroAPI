using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MiscSalesInvoiceController : ControllerBase
    {
        private readonly IMiscSalesInvoiceService _IMiscSalesinvoiceService;

        public MiscSalesInvoiceController(IMiscSalesInvoiceService IMiscSalesinvoiceService)
        {
            _IMiscSalesinvoiceService = IMiscSalesinvoiceService;
        }


        [HttpPost]
        [Route("getItems")]
        public getItemsResponse GetSaleInvoiceHeaderData(getItemsInput input)
        {
            getItemsResponse res = new getItemsResponse();

            try
            {
                res = _IMiscSalesinvoiceService.getItems(input);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
                
            }

            return res;
        }


        [HttpPost]
        [Route("insert")]
        public MiscSalesInvoiceResponse Insert(MiscSalesInvoiceSave input)
        {
            MiscSalesInvoiceResponse res = new MiscSalesInvoiceResponse();

            try
            {
                res = _IMiscSalesinvoiceService.insert(input);
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
                res = _IMiscSalesinvoiceService.Update(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        //[HttpPost]
        //[Route("list")]
        //public TransferGridResponse GetTransferData(TransferInvoiceRequest request)
        //{
        //    TransferGridResponse res = new TransferGridResponse();

        //    try
        //    {
        //        res = _IMiscSalesinvoiceService.GetTransferData(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.flag = 0;
        //        res.Message = "Error: " + ex.Message;
        //        res.Data = new List<TransferGridItem>();
        //    }

        //    return res;
        //}
        [HttpPost]
        [Route("list")]
        public MiscSalesInvoiceResponse getMisSalesInvoiceData(InvoiceListRequest request)
        {
            MiscSalesInvoiceResponse res = new MiscSalesInvoiceResponse();

            try
            {
                res = _IMiscSalesinvoiceService.getMisSalesInvoiceData(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public MiscSalesInvoiceSave Select(int id)
        {
            MiscSalesInvoiceSave response = new MiscSalesInvoiceSave();
            try
            {
                response = _IMiscSalesinvoiceService.GetMiscSaleInvoiceById(id);
            }
            catch (Exception ex)
            {
               
            }
            return response;
        }
        [HttpPost]
        [Route("commit")]
        public InvoiceResponse Commit(InvoiceUpdate model)
        {
            InvoiceResponse response = new InvoiceResponse();
            try
            {
                response = _IMiscSalesinvoiceService.commit(model);
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
                res = _IMiscSalesinvoiceService.GetInvoiceNo();
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
                res = _IMiscSalesinvoiceService.Delete(id);
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
        public List<InvoiceCust_stateName> Getcustlist(InvoiceListRequest request)
        {
            List<InvoiceCust_stateName> customers = new List<InvoiceCust_stateName>();

            try
            {

                customers = _IMiscSalesinvoiceService.Getcustlist(request);
            }
            catch (Exception ex)
            {
            }
            return customers.ToList();
        }
    }
}
