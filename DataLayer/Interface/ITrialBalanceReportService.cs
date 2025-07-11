using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITrialBalanceReportService
    {
     List<TrialBalanceReport> GetTrialBalanceReport(int companyId, int finId, DateTime dateFrom, DateTime dateTo);

    }
}
