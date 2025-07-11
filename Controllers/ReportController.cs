using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _ReportService;
        public ReportController(IReportService ReportService)
        {
            _ReportService = ReportService;
        }
        [HttpPost]
        [Route("initData")]
        public LedgerReportInitData InitData(int companyId)
        {
            LedgerReportInitData res = new LedgerReportInitData();

            try
            {
                res = _ReportService.GetInitData(companyId);
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
        [HttpPost("ledger")]
        public LedgerStatementResponse GetLedgerStatement(Report request)
        {
            var res = new LedgerStatementResponse();
            try
            {
                res = _ReportService.GetLedgerStatement(request);
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
    }
}
