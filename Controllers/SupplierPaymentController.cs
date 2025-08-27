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
        [HttpPost]
        [Route("select/{id:int}")]
        public SupplierSelectResponse Select(int id)
        {
            SupplierSelectResponse response = new SupplierSelectResponse();
            try
            {
                response = _supplierService.GetSupplierById(id);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("invoicelist")]
        public PendingInvoiceResponse GetPendingInvoiceList(PendingInvoiceRequest request)
        {
            PendingInvoiceResponse res = new PendingInvoiceResponse();

            try
            {
                res = _supplierService.GetPendingInvoiceList(request);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
                res.Data = new List<PendingInvoicelist>();
            }

            return res;
        }
        [HttpPost]
        [Route("commit")]
        public SupplierPaymentResponse Commit(SupplierPaymentUpdate model)
        {
            SupplierPaymentResponse response = new SupplierPaymentResponse();
            try
            {
                response = _supplierService.commit(model);
            }
            catch (Exception ex)
            {
                response.flag = 0;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("supplierno")]
        public SupplierVoucherResponse GetSupplierNo()
        {
            SupplierVoucherResponse res = new SupplierVoucherResponse();

            try
            {
                res = _supplierService.GetSupplierNo();
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.Message = "Error: " + ex.Message;
            }

            return res;
        }
        [HttpPost("GetPDCListBySupplierId/{supplierId}")]
        public IActionResult GetPDCListBySupplierId(int supplierId)
        {
            var response = _supplierService.GetPDCListBySupplierId(supplierId);
            if (response.flag == 1)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}
