using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
using MicroApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BankReconciliationController : ControllerBase
    {
        private readonly IBankReconciliationService _bankReconciliationService;
        public BankReconciliationController(IBankReconciliationService bankReconciliationService)
        {
            _bankReconciliationService = bankReconciliationService;
        }
        [HttpPost]
        [Route("bankrecon")]
        public BankReconciliationReportResponse GetBankReconciliationReport(BankReconciliation request)
        {
            var res = new BankReconciliationReportResponse();
            try
            {
                res = _bankReconciliationService.GetBankReconciliationReport(request);
                res.flag = 1;
                res.message = "Success";
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }
            return res;
        }
        [HttpPost]
        [Route("save")]
        public BankReconciliationSaveResponse SaveBankReconciliation(BankReconciliationInput request)
        {
            var res = new BankReconciliationSaveResponse();
            try
            {
                res = _bankReconciliationService.SaveBankReconciliation(request);
                
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
            }
            return res;
        }
    
    }
}
