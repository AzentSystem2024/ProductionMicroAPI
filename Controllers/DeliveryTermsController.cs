using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace RetailApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryTermsController : ControllerBase
    {
        private readonly IDeliveryTermsService _deliveryTermsService;
        public DeliveryTermsController(IDeliveryTermsService deliveryTermsService)
        {
            _deliveryTermsService = deliveryTermsService;
        }

        [HttpPost]
        [Route("list")]
        public List<DeliveryTerms> List()
        {
            List<DeliveryTerms> deliveryTerms = new List<DeliveryTerms>();

            try
            {
                
                deliveryTerms = _deliveryTermsService.GetAllDeliveryTerms();
            }
            catch (Exception ex)
            {
            }
            return deliveryTerms.ToList();
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public DeliveryTerms Select(int id)
        {
            DeliveryTerms objDeliveryTerms = new DeliveryTerms();
            try
            {
                
                objDeliveryTerms = _deliveryTermsService.GetItems(id);
            }
            catch (Exception ex)
            {

            }

            return objDeliveryTerms;
        }

        [HttpPost]
        [Route("save")]
        public DeliveryTermsResponse SaveData(DeliveryTerms deliveryTermsData)
        {
            DeliveryTermsResponse res = new DeliveryTermsResponse();

            try
            {
                
                Int32 ID = _deliveryTermsService.SaveData(deliveryTermsData);

                res.flag = "1";
                res.message = "Success";
                res.data = _deliveryTermsService.GetItems(ID);
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
        public DeliveryTermsResponse Delete(int id)
        {
            DeliveryTermsResponse res = new DeliveryTermsResponse();

            try
            {
                

                _deliveryTermsService.DeleteDeliveryTerms(id);
                res.flag = "1";
                res.message = "Success";
                res.data = _deliveryTermsService.GetItems(id);
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
