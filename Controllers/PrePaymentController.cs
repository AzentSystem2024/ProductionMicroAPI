using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrePaymentController : ControllerBase
    {
        private readonly IPrePaymentService _prepaymentService;

        public PrePaymentController(IPrePaymentService prepaymentService)
        {
            _prepaymentService = prepaymentService;
        }
        [HttpPost]
        [Route("insert")]
        public PrePaymentResponse Save(PrePayment model)
        {
            PrePaymentResponse res = new PrePaymentResponse();

            try
            {
                res = _prepaymentService.Save(model);
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
        public PrePaymentResponse Update(PrePaymentUpdate model)
        {
            PrePaymentResponse res = new PrePaymentResponse();
            try
            {
                res = _prepaymentService.Update(model);
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
        public PrePaymentListResponse GetPrePaymentList()
        {
            PrePaymentListResponse res = new PrePaymentListResponse();

            try
            {
                res = _prepaymentService.GetPrePaymentList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PrePaymentListHeader>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public PrePaymentListHeaderResponse Select(int id)
        {
            PrePaymentListHeaderResponse response = new PrePaymentListHeaderResponse();
            try
            {
                response = _prepaymentService.GetPrePaymentById(id);
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
