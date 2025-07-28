using MicroApi.DataLayer.Interface;
using MicroApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AC_ReportController : ControllerBase
    {
        private readonly IAC_ReportService _ReportService;
        public AC_ReportController(IAC_ReportService ReportService)
        {
            _ReportService = ReportService;
        }
        [HttpPost]
        [Route("initData")]
        public LedgerReportInitData InitData(InitDataRequest request)
        {
            LedgerReportInitData res = new LedgerReportInitData();

            try
            {
                res = _ReportService.GetInitData(request.COMPANY_ID);
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
        public LedgerStatementResponse GetLedgerStatement(AC_Report request)
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
        [HttpPost("articleproduction")]
        public ArticleProductionResponse GetArticleProductionReport(ArticleProductionFilter request)
        {
            var res = new ArticleProductionResponse();
            try
            {
                res = _ReportService.GetArticleProductionReport(request);
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
