using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPayrollService
    {
        PayrollReportResponse GetPayrollReport(PayrollReportRequest request);
        PayrollOTResponse GetPayrollOTReport(PayrollReportRequest request);
        List<PaymentModeModel> GetPaymentMode();
    }

}