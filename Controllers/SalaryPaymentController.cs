using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryPaymentController : ControllerBase
    {
        private readonly ISalaryPaymentService _salaryService;

        public SalaryPaymentController(ISalaryPaymentService salaryService)
        {
            _salaryService = salaryService;
        }
        [HttpPost]
        [Route("insert")]
        public SalaryPaymentResponse Insert(SalaryPayment model)
        {
            SalaryPaymentResponse res = new SalaryPaymentResponse();

            try
            {
                res = _salaryService.Insert(model); 
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("list")]
        public SalaryPendingResponse GetPendingSalaryList(SalaryPendingRequest request)
        {
            SalaryPendingResponse res = new SalaryPendingResponse();

            try
            {
                res = _salaryService.GetPendingSalaryList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = "Error: " + ex.Message;
                res.data = null;
            }

            return res;
        }
        [HttpPost]
        [Route("view")]
        public SalaryPaymentListResponse GetsalaryPaymentList(SalaryPendingListRequest request)
        {
            SalaryPaymentListResponse res = new SalaryPaymentListResponse();

            try
            {
                res = _salaryService.GetsalaryPaymentList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SalaryPaymentListItem>();
            }

            return res;
        }
        [HttpPost]
        [Route("paymentno")]
        public SalPayLastDocno GetLastDocNo()
        {
            SalPayLastDocno res = new SalPayLastDocno();

            try
            {
                res = _salaryService.GetLastDocNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("select/{id:int}")]
        public SalaryPaymentDetailResponse Select(int id)
        {
            SalaryPaymentDetailResponse response = new SalaryPaymentDetailResponse();
            try
            {
                response = _salaryService.GetSalaryPaymentById(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("update")]
        public SalaryPaymentResponse update(SalaryPaymentUpdate model)
        {
            SalaryPaymentResponse res = new SalaryPaymentResponse();

            try
            {
                res = _salaryService.update(model);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost]
        [Route("commit")]
        public SalaryPaymentResponse commit(SalaryPaymentUpdate model)
        {
            SalaryPaymentResponse res = new SalaryPaymentResponse();

            try
            {
                res = _salaryService.commit(model);
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
        public SalaryPaymentResponse Delete(int id)
        {
            SalaryPaymentResponse res = new SalaryPaymentResponse();
            try
            {
                res = _salaryService.Delete(id);
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
