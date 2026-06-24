using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroApi.DataLayer.Interface;
using MicroApi.Models;

namespace MicroApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ARManualMatchingController : ControllerBase
    {
        private readonly IARManualMatchingService _ARManualMatchingService;
        public ARManualMatchingController(IARManualMatchingService ARManualMatchingService)
        {
            _ARManualMatchingService = ARManualMatchingService;
        }


      

        [HttpPost]
        [Route("receiptlist")]
        public ARReceiptResponse receiptList(ARReceiptInput vinput)
        {
            ARReceiptResponse res = new ARReceiptResponse();

            try
            {

                res = _ARManualMatchingService.receiptList(vinput);

            }
            catch (Exception ex)
            {
                res.flag =  "0";
                res.message = ex.Message;
            }

            return res;
        }

        [HttpPost]
        [Route("invoicelist")]
        public ARInvoiceResponse invoiceList(ARInvoiceInput vinput)
        {
            ARInvoiceResponse res = new ARInvoiceResponse();

            try
            {

                res = _ARManualMatchingService.invoiceList(vinput);

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
