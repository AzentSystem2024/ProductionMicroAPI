using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IGSTReportService
    {
        GSTReportResponse GetGSTReport(GSTReportRequest request);
        GSTB2CLReportResponse GetGSTB2CLReport(GSTReportRequest request);
        GSTReportResponse GetGSTReportCDNR(GSTReportRequest request);
    }
}
