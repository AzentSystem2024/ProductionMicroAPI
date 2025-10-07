using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Delivery_ReturnController : ControllerBase
    {
        private readonly IDelivery_ReturnService _delivery_ReturnService;
        public Delivery_ReturnController(IDelivery_ReturnService delivery_ReturnService)
        {
            _delivery_ReturnService = delivery_ReturnService;
        }
        [HttpPost]
        [Route("insert")]
        public DeliveryReturnsaveResponse Insert(Delivery_Return deliveryreturn)
        {
            DeliveryReturnsaveResponse res = new DeliveryReturnsaveResponse();

            try
            {

                _delivery_ReturnService.Insert(deliveryreturn);
                res.flag = 1;
                res.Message = "Success";
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
        public DeliveryReturnsaveResponse Update(Delivery_ReturnUpdate deliveryreturn)
        {
            DeliveryReturnsaveResponse res = new DeliveryReturnsaveResponse();

            try
            {

                _delivery_ReturnService.Update(deliveryreturn);
                res.flag = 1;
                res.Message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("getdn")]
        public DeliverynoteDetailResponse GetDN(DNRequest request)
        {
            DeliverynoteDetailResponse res = new DeliverynoteDetailResponse();

            try
            {
                var result = _delivery_ReturnService.GetDN(request);

                res.Flag = 1;
                res.Message = "Success";
                res.Data = result;
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
        public DeliveryRtnListResponse List()
        {
            try
            {
                DeliveryRtnListResponse res = _delivery_ReturnService.GetDeliveryRtnList();
                return res;
            }
            catch (Exception ex)
            {
                return new DeliveryRtnListResponse
                {
                    flag = 0,
                    Message = ex.Message,
                    Data = new List<DeliveryRtnList>()
                };
            }
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public DeliveryRtnSelectResponse Select(int id)
        {
            DeliveryRtnSelectResponse res = new DeliveryRtnSelectResponse();
            try
            {
                res = _delivery_ReturnService.GetDeliveryRtnById(id);
            }
            catch (Exception ex)
            {
            }
            return res;
        }

    }
}
