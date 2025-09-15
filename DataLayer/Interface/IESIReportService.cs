using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IESIReportService
    {
        ESIReportResponse GetESIReport(ESIReportRequest request);
    }
}
