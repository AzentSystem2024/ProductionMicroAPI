using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IReportService
    {
        LedgerReportInitData GetInitData(int companyId);
        LedgerStatementResponse GetLedgerStatement(Report request);


    }
}
