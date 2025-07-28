using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IAC_ReportService
    {
        LedgerReportInitData GetInitData(int companyId);
        LedgerStatementResponse GetLedgerStatement(AC_Report request);
        ArticleProductionResponse GetArticleProductionReport(ArticleProductionFilter request);


    }
}
