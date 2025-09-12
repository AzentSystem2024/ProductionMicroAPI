using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IWageRegReportService
    {
        WageReportResponse GetWageReport(WageReportRequest request);
    }
}
