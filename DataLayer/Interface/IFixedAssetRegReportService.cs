using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IFixedAssetRegReportService
    {
        FixedAssetReportResponse GetFixedAssetReport(FixedAssetReportRequest request);
    }
}
