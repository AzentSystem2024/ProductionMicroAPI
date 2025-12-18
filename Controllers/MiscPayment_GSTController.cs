using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscPayment_GSTController : ControllerBase
    {
        private readonly IMiscPayment_GSTService _miscpaymentgstService;
        public MiscPayment_GSTController(IMiscPayment_GSTService miscpaymentgstService)
        {
            _miscpaymentgstService = miscpaymentgstService;
        }

        [HttpPost]
        [Route("insert")]
        public MiscpaymentGSTResponse Save(MiscPayment_GST model)
        {
            MiscpaymentGSTResponse res = new MiscpaymentGSTResponse();

            try
            {
                res = _miscpaymentgstService.Save(model);
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
        public MiscpaymentGSTResponse Update(MiscPayment_GSTUpdate model)
        {
            MiscpaymentGSTResponse res = new MiscpaymentGSTResponse();

            try
            {
                res = _miscpaymentgstService.Edit(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Update Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public MiscPaymentListGSTResponse GetMiscPaymentList(MiscpaymentGSTListRequest request)
        {
            MiscPaymentListGSTResponse res = new MiscPaymentListGSTResponse();

            try
            {
                res = _miscpaymentgstService.GetMiscPaymentList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<MiscPaymentListGSTItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public MiscPaymentGSTSelectedView Select(int id)
        {
            MiscPaymentGSTSelectedView response = new MiscPaymentGSTSelectedView();
            try
            {
                response = _miscpaymentgstService.GetMiscPaymentById(id);
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
        public MiscpaymentGSTResponse Commit(MiscPayment_GSTUpdate model)
        {
            MiscpaymentGSTResponse response = new MiscpaymentGSTResponse();
            try
            {
                response = _miscpaymentgstService.commit(model);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("paymentno")]
        public MiscGSTLastDocno GetLastDocNo()
        {
            MiscGSTLastDocno res = new MiscGSTLastDocno();

            try
            {
                res = _miscpaymentgstService.GetLastDocNo();
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
        public MiscpaymentGSTResponse Delete(int id)
        {
            MiscpaymentGSTResponse res = new MiscpaymentGSTResponse();
            try
            {
                res = _miscpaymentgstService.Delete(id);
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
