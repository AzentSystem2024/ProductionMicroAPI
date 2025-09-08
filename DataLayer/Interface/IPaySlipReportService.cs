using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPaySlipReportService
    {
        PayslipReportResponse GetPayslipReport(PayslipReportRequest request);
    }
}
