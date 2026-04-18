using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MiscPurchInvoiceController : ControllerBase
    {
        private readonly IMiscPurchInvoiceService _miscPurchInvoiceService;
        public MiscPurchInvoiceController(IMiscPurchInvoiceService miscPurchInvoiceService)
        {
            _miscPurchInvoiceService = miscPurchInvoiceService;
        }
        [HttpPost]
        [Route("insert")]
        public MiscPurchResponse Insert(MiscPurchInvoice Data)
        {
            MiscPurchResponse res = new MiscPurchResponse();

            try
            {

                _miscPurchInvoiceService.Insert(Data);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("update")]
        public MiscPurchResponse Update(MiscPurchInvoice Data)
        {
            MiscPurchResponse res = new MiscPurchResponse();

            try
            {

                _miscPurchInvoiceService.Update(Data);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("Commit")]
        public MiscPurchResponse Commit(MiscPurchInvoice Data)
        {
            MiscPurchResponse res = new MiscPurchResponse();

            try
            {

                _miscPurchInvoiceService.Commit(Data);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public MiscPurchResponse GetMiscPurchList(PurchListRequest request)
        {
            MiscPurchResponse res = new MiscPurchResponse();

            try
            {
                List<MiscPurchList> miscpurchInvoice = _miscPurchInvoiceService.GetMiscPurchList(request);

                res.Flag = 1;
                res.Message = "Success";
                res.List = miscpurchInvoice;
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.List = new List<MiscPurchList>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public MiscPurchResponse Select(int id)
        {
            MiscPurchResponse res = new MiscPurchResponse();

            try
            {
                res.Data = _miscPurchInvoiceService.GetMiscPurchById(id);
                res.Flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.Flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public MiscPurchResponse Delete(int id)
        {
            MiscPurchResponse res = new MiscPurchResponse();
            try
            {
                res = _miscPurchInvoiceService.Delete(id);
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
