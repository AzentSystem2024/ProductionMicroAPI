using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IBankReconciliationService
    {
        BankReconciliationReportResponse GetBankReconciliationReport(BankReconciliation request);
        BankReconciliationSaveResponse SaveBankReconciliation(BankReconciliationInput request);

    }
}
