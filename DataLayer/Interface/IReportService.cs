using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IReportService
    {
        PDCListReportResponse GetPDCList(PDCListReportRequest request);
        FixedAssetReportResponse GetFixedAssetReport(FixedAssetReportRequest request);
        DepreciationReportResponse GetDepreciationReport(DepreciationReportRequest request);
        PrepaymentReportResponse GetPrepaymentReport(PrepaymentReportRequest request);
        ProfitLossBranchResponse GetProfitLossBranch(ProfitLossBranchRequest request);
    }
}
