using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAC_ReportService
    {
        LedgerReportInitData GetInitData(int companyId);
        LedgerStatementResponse GetLedgerStatement(AC_Report request);
        ArticleProductionResponse GetArticleProductionReport(ArticleProductionFilter request);
        BoxProductionResponse GetBoxProductionReport(BoxProductionFilter request);
        CashBookResponse GetCashBookReport(CashBookFilter request);
        BalanceSheetResponse GetBalanceSheetReport(BalanceSheetFilter request);
        ProfitLossReportResponse GetProfitLossReport(ProfitLossReportRequest request);
        SupplierStatReportResponse GetSupplierStateReports(SupplierStatReportRequest request);
        AgedPayableReportResponse GetAgedPayableReports(AgedPayableReportRequest request);

    }
}
