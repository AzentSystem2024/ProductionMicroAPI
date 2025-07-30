using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierPaymentController : ControllerBase
    {
       
        private readonly ISupplierPaymentService _supplierService;

        public SupplierPaymentController(ISupplierPaymentService supplierService)
        {
            _supplierService = supplierService;
        }
        [HttpPost]
        [Route("insert")]
        public SupplierPaymentResponse Insert(SupplierPayment model)
        {
            SupplierPaymentResponse res = new SupplierPaymentResponse();

            try
            {
                res = _supplierService.insert(model);
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
        public SupplierPaymentResponse Update(SupplierPaymentUpdate model)
        {
            SupplierPaymentResponse res = new SupplierPaymentResponse();

            try
            {
                res = _supplierService.Update(model);
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
        public SupplierPaymentListResponse GetPaymentList()
        {
            SupplierPaymentListResponse res = new SupplierPaymentListResponse();

            try
            {
                res = _supplierService.GetPaymentList();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<SupplierPaymentListItem>();
            }

            return res;
        }
    }
}
