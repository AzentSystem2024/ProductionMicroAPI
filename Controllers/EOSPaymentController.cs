using MicroApi.DataLayer.Interface;
using MicroApi.Helper;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MicroApi.Controllers
{
    //[Authorize]
    
    [ApiController]
    [Route("api/[controller]")]
    public class EOSPaymentController : ControllerBase
    {
        private readonly IEmployeeEOSPaymentService service;

        public EOSPaymentController(IEmployeeEOSPaymentService _service)
        {
            service = _service;
        }

        [HttpPost]
        [Route("list")]
        public IActionResult List()
        {
            return Ok(service.GetAllEmployeeEOSPayments());
        }

        [HttpPost]
        [Route("select/{id:int}")]
        public IActionResult Select(int id)
        {
            return Ok(service.SelectEmployeeEOSPaymentData(id));
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save(SaveEmployeeEOSPaymentData model)
        {
            return Ok(service.SaveData(model));
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(SaveEmployeeEOSPaymentData model)
        {
            return Ok(service.UpdateData(model));
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteEmployeeEOSPayment(id));
        }

        [HttpPost]
        [Route("verify")]
        public IActionResult Verify(SaveEmployeeEOSPaymentData model)
        {
            return Ok(service.VerifyData(model));
        }

        [HttpPost]
        [Route("approve")]
        public IActionResult Approve(SaveEmployeeEOSPaymentData model)
        {
            return Ok(service.ApproveData(model));
        }

        //[HttpPost]
        //[Route("unapprove/{id:int}")]
        //public IActionResult UnApprove(int id)
        //{
        //    return Ok(service.UnApproveEOSPayment(id));
        //}

        //[HttpPost]
        //[Route("reject/{id:int}")]
        //public IActionResult Reject(int id)
        //{
        //    return Ok(service.RejectEOSPayment(id));
        //}

        //[HttpPost]
        //[Route("cancel/{id:int}")]
        //public IActionResult Cancel(int id)
        //{
        //    return Ok(service.CancelEOSPayment(id));
        //}

        //[HttpPost]
        //[Route("EOSPayment")]
        //public IActionResult GetEmployeeEOSPayment(EOSPaymentRequest request)
        //{
        //    return Ok(service.GetEmployeeEOSPayment(request));
        //}
    }
}

