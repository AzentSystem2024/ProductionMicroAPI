using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Delivery_NoteController : ControllerBase
    {
        private readonly IDelivery_NoteService _delivery_noteservice;
        public Delivery_NoteController(IDelivery_NoteService delivery_noteservice)
        {
            _delivery_noteservice = delivery_noteservice;
        }
        [HttpPost]
        [Route("insert")]
        public DeliverynotesaveResponse Insert(Delivery_Note deliverynote)
        {
            DeliverynotesaveResponse res = new DeliverynotesaveResponse();

            try
            {

                _delivery_noteservice.Insert(deliverynote);
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
        public DeliverynotesaveResponse Update(Delivery_NoteUpdate deliverynote)
        {
            DeliverynotesaveResponse res = new DeliverynotesaveResponse();

            try
            {

                _delivery_noteservice.Update(deliverynote);
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
        [Route("getso")]
        public SODetailResponse GetSO()
        {
            SODetailResponse res = new SODetailResponse();

            try
            {
                var result = _delivery_noteservice.GetSO();

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
        public Delivery_Note_List_Response List()
        {
            try
            {
                Delivery_Note_List_Response res = _delivery_noteservice.GetDeliveryNoteList();
                return res;
            }
            catch (Exception ex)
            {
                return new Delivery_Note_List_Response
                {
                    flag = 0,
                    Message = ex.Message,
                    Data = new List<Delivery_Note_List>()
                };
            }
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public Delivery_Note_Select_Response Select(int id)
        {
            Delivery_Note_Select_Response res = new Delivery_Note_Select_Response();
            try
            {
                res = _delivery_noteservice.GetDeliveryNoteById(id);
            }
            catch (Exception ex)
            {
            }
            return res;
        }
        [HttpPost]
        [Route("approve")]
        public DeliverynotesaveResponse Approve(Delivery_NoteUpdate deliverynote)
        {
            DeliverynotesaveResponse res = new DeliverynotesaveResponse();

            try
            {

                _delivery_noteservice.Approve(deliverynote);
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
        [Route("delete/{id:int}")]
        public DeliverynotesaveResponse Delete(int id)
        {
            DeliverynotesaveResponse res = new DeliverynotesaveResponse();

            try
            {


                _delivery_noteservice.Delete(id);
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
        [Route("deliveryno")]
        public DeliveryDoc GetLastDocNo()
        {
            DeliveryDoc res = new DeliveryDoc();

            try
            {
                res = _delivery_noteservice.GetLastDocNo();
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
