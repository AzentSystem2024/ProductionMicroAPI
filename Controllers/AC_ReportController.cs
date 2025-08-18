using MicroApi.DataLayer.Interface;
using MicroApi.DataLayer.Service;
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

        [HttpPost("Boxproduction")]
        public BoxProductionResponse GetBoxProductionReport(BoxProductionFilter request)
        {
            var res = new BoxProductionResponse();
            try
            {
                res = _ReportService.GetBoxProductionReport(request);
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
        [HttpPost("CashBook")]
        public CashBookResponse GetCashBookReport(CashBookFilter request)
        {
            var res = new CashBookResponse();
            try
            {
                res = _ReportService.GetCashBookReport(request);
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
        [HttpPost("BalanceSheet")]
        public BalanceSheetResponse GetBalanceSheetReport(BalanceSheetFilter request)
        {
            var res = new BalanceSheetResponse();
            try
            {
                res = _ReportService.GetBalanceSheetReport(request);
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

        [HttpPost("profitloss")]
        public ProfitLossReportResponse GetProfitlossReport(ProfitLossReportRequest request)
        {
            var res = new ProfitLossReportResponse();
            try
            {
                res = _ReportService.GetProfitLossReport(request);
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
        [HttpPost("SupplierStatement")]
        public IActionResult GetSupplierStateReports(SupplierStatReportRequest request)
        {
            var res = new SupplierStatReportResponse();
            try
            {
                res = _ReportService.GetSupplierStateReports(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.flag = 0;
                res.message = ex.Message;
                return BadRequest(res);
            }
        }
        [HttpPost("AgedPayable")]
        public AgedPayableReportResponse GetAgedPayableReport(AgedPayableReportRequest request)
        {
            var res = new AgedPayableReportResponse();
            try
            {
                res = _ReportService.GetAgedPayableReports(request);
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

