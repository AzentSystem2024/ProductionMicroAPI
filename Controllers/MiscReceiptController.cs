using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscReceiptController : ControllerBase
    {
        private readonly IMiscReceiptService _miscreceiptService;

        public MiscReceiptController(IMiscReceiptService miscreceiptService)
        {
            _miscreceiptService = miscreceiptService;
        }
        [HttpPost]
        [Route("insert")]
        public MiscReceiptResponse Insert(MiscReceipt model)
        {
            MiscReceiptResponse res = new MiscReceiptResponse();

            try
            {
                res = _miscreceiptService.Insert(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("update")]
        public MiscReceiptResponse Update(MiscReceiptUpdate model)
        {
            MiscReceiptResponse res = new MiscReceiptResponse();
            try
            {
                res = _miscreceiptService.Update(model);
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
        public MiscReceiptListResponse GetReceiptList(MiscReceiptsListRequest request)
        {
            MiscReceiptListResponse res = new MiscReceiptListResponse();
            try
            {
                res = _miscreceiptService.GetReceiptList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
                res.Data = new List<MiscReceiptListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public MiscReceiptResponse GetMiscReceiptById(int id)
        {
            MiscReceiptResponse res = new MiscReceiptResponse();

            try
            {
                res = _miscreceiptService.GetMiscReceiptById(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = null;
            }

            return res;
        }
        [HttpPost]
        [Route("voucherno")]
        public LasrVoucherResponse GetLastVoucherNo()
        {
            LasrVoucherResponse res = new LasrVoucherResponse();

            try
            {
                res = _miscreceiptService.GetLastVoucherNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.VOUCHER_NO = null;
            }

            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public MiscReceiptResponse Delete(int id)
        {
            MiscReceiptResponse res = new MiscReceiptResponse();
            try
            {
                res = _miscreceiptService.Delete(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("commit")]
        public MiscReceiptResponse commit(MiscReceiptUpdate model)
        {
            MiscReceiptResponse res = new MiscReceiptResponse();
            try
            {
                res = _miscreceiptService.commit(model);
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
