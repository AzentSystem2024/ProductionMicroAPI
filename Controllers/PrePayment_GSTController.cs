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
    public class PrePayment_GSTController : ControllerBase
    {
        private readonly IPrePayment_GSTService _prepayment_gstService;

        public PrePayment_GSTController(IPrePayment_GSTService prepayment_gstService)
        {
            _prepayment_gstService = prepayment_gstService;
        }
        [HttpPost]
        [Route("insert")]
        public PrePayment_GSTResponse Save(PrePayment_GST model)
        {
            PrePayment_GSTResponse res = new PrePayment_GSTResponse();

            try
            {
                res = _prepayment_gstService.Save(model);
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
        public PrePayment_GSTResponse Update(PrePayment_GSTUpdate model)
        {
            PrePayment_GSTResponse res = new PrePayment_GSTResponse();
            try
            {
                res = _prepayment_gstService.Update(model);
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
        public PrePayment_GSTListResponse GetPrePaymentList(PrePaymentGSTListRequest request)
        {
            PrePayment_GSTListResponse res = new PrePayment_GSTListResponse();

            try
            {
                res = _prepayment_gstService.GetPrePaymentList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PrePayment_GSTListHeader>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public PrePayment_GSTListHeaderResponse Select(int id)
        {
            PrePayment_GSTListHeaderResponse response = new PrePayment_GSTListHeaderResponse();
            try
            {
                response = _prepayment_gstService.GetPrePaymentById(id);
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
        public PrePayment_GSTResponse commit(PrePayment_GSTUpdate model)
        {
            PrePayment_GSTResponse res = new PrePayment_GSTResponse();
            try
            {
                res = _prepayment_gstService.commit(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("delete/{id:int}")]
        public PrePayment_GSTResponse Delete(int id)
        {
            PrePayment_GSTResponse res = new PrePayment_GSTResponse();
            try
            {
                res = _prepayment_gstService.Delete(id);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("invoiceno")]
        public PrePaymentGSTLastDocno GetLastDocNo()
        {
            PrePaymentGSTLastDocno res = new PrePaymentGSTLastDocno();

            try
            {
                res = _prepayment_gstService.GetLastDocNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
    }
}
