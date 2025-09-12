using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEPFReportService
    {
        EPFReportResponse GetEPFReport(EPFReportRequest request);
    }
}
