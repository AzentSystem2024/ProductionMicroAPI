using MicroApi.Models;

public interface ITrialBalanceReportService
{
    List<TrialBalanceReport> GetTrialBalanceReport(
        int companyId,
        int finId,
        DateTime dateFrom,
        DateTime dateTo
    );

    List<TrialBalanceDimensionReport> GetTrialBalanceDimensionReport(
        int companyId,
        int finId,
        DateTime dateFrom,
        DateTime dateTo,
        string dimensionCode
    );

    List<TrialBalanceAsOnDate> GetTrialBalanceAsOnDate(
        int companyId,
        int finId,
        DateTime dateTo,
    string storeIds
    );
}