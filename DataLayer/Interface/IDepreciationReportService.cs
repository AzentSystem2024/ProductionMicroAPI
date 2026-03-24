using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepreciationReportService
    {
        DepreciationReportResponse GetDepreciationReport(DepreciationReportRequest request);
    }
}
