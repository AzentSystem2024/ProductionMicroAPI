using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MiscPaymentController : ControllerBase
    {
        private readonly IMiscPaymentService _miscpaymentService;

        public MiscPaymentController(IMiscPaymentService miscpaymentService)
        {
            _miscpaymentService = miscpaymentService;
        }

        [HttpPost]
        [Route("insert")]
        public MiscpaymentResponse Save(MiscPayment model)
        {
            MiscpaymentResponse res = new MiscpaymentResponse();

            try
            {
                res = _miscpaymentService.Save(model);
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
        public MiscpaymentResponse Update(MiscPaymentUpdate model)
        {
            MiscpaymentResponse res = new MiscpaymentResponse();

            try
            {
                res = _miscpaymentService.Edit(model);
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
        public MiscPaymentListResponse GetMiscPaymentList(MiscpaymentListRequest request)
        {
            MiscPaymentListResponse res = new MiscPaymentListResponse();

            try
            {
                res = _miscpaymentService.GetMiscPaymentList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<MiscPaymentListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public MiscPaymentSelectedView Select(int id)
        {
            MiscPaymentSelectedView response = new MiscPaymentSelectedView();
            try
            {
                response = _miscpaymentService.GetMiscPaymentById(id);
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
        public MiscpaymentResponse Commit(MiscPaymentUpdate model)
        {
            MiscpaymentResponse response = new MiscpaymentResponse();
            try
            {
                response = _miscpaymentService.commit(model);
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
        public MiscLastDocno GetLastDocNo()
        {
            MiscLastDocno res = new MiscLastDocno();

            try
            {
                res = _miscpaymentService.GetLastDocNo();
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
        public MiscpaymentResponse Delete(int id)
        {
            MiscpaymentResponse res = new MiscpaymentResponse();
            try
            {
                res = _miscpaymentService.Delete(id);
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
