using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrePayment_PostingController : ControllerBase
    {
        private readonly IPrePayment_PostingService _prePayment_PostingService;

        public PrePayment_PostingController(IPrePayment_PostingService prePayment_Posting)
        {
            _prePayment_PostingService = prePayment_Posting;
        }
        [HttpPost]
        [Route("list")]
        public PrePayment_RequestResponse GetPrePaymentPendingList(PrePayment_PostingRequest request)
        {
            PrePayment_RequestResponse res = new PrePayment_RequestResponse();

            try
            {
                res = _prePayment_PostingService.GetPrePaymentPendingList(request);
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
        [Route("insert")]
        public PrepaymentPostingResponse Save(PrePayment_Posting model)
        {
            PrepaymentPostingResponse res = new PrepaymentPostingResponse();

            try
            {
                res = _prePayment_PostingService.Save(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("Edit")]
        public PrepaymentPostingResponse Edit(PrePayment_PostingEdit model)
        {
            PrepaymentPostingResponse res = new PrepaymentPostingResponse();

            try
            {
                res = _prePayment_PostingService.Edit(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("prepaylist")]
        public PrePayment_PostingListResponse GetPrePaymentList()
        {
            PrePayment_PostingListResponse res = new PrePayment_PostingListResponse();

            try
            {
                res = _prePayment_PostingService.GetPrePaymentList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PrePayment_PostingListHeader>();
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public PostingSelectResponse Select(int id)
        {
            PostingSelectResponse response = new PostingSelectResponse();
            try
            {
                response = _prePayment_PostingService.GetPrePaymentById(id);
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
        public PrepaymentPostingResponse commit(PrePayment_PostingEdit model)
        {
            PrepaymentPostingResponse res = new PrepaymentPostingResponse();

            try
            {
                res = _prePayment_PostingService.commit(model);
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
        public PrepaymentPostingResponse Delete(int id)
        {
            PrepaymentPostingResponse res = new PrepaymentPostingResponse();
            try
            {
                res = _prePayment_PostingService.Delete(id);
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
