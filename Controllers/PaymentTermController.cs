using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTermController : ControllerBase
    {
        private readonly IPaymentTermService _paymentTermService;
        public PaymentTermController(IPaymentTermService paymentTermService)
        {
            _paymentTermService = paymentTermService;
        }
        [HttpPost]
        [Route("list")]
        public List<PaymentTerms> List()
        {
            List<PaymentTerms> paymentTerms = new List<PaymentTerms>();

            try
            {
                
                paymentTerms = _paymentTermService.GetAllPaymentTerms();
            }
            catch (Exception ex)
            {
            }
            return paymentTerms.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public PaymentTerms Select(int id)
        {
            PaymentTerms objPaymentTerms = new PaymentTerms();
            try
            {
                
                objPaymentTerms = _paymentTermService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objPaymentTerms;
        }

        [HttpPost]
        [Route("save")]
        public PaymentTermsResponse SaveData(PaymentTerms paymentTermsData)
        {
            PaymentTermsResponse res = new PaymentTermsResponse();

            try
            {
                
                Int32 ID = _paymentTermService.SaveData(paymentTermsData);

                res.flag = "1";
                res.message = "Success";
                res.data = _paymentTermService.GetItems(ID);
            }
            catch (Exception ex)
            {
                res.flag = "1";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public PaymentTermsResponse Delete(int id)
        {
            PaymentTermsResponse res = new PaymentTermsResponse();

            try
            {
                

                _paymentTermService.DeletePaymentTerms(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _paymentTermService.GetItems(id);
            }
            catch (Exception ex)
            {
                res.flag = "0";
                res.message = ex.Message;
            }
            return res;
        }
    }
}
