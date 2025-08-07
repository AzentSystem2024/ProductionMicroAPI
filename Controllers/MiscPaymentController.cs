using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
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
        public MiscPaymentListResponse GetMiscPaymentList()
        {
            MiscPaymentListResponse res = new MiscPaymentListResponse();

            try
            {
                res = _miscpaymentService.GetMiscPaymentList();
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
    }
}
