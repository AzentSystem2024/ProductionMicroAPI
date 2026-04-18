using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalesReportService
    {
        SalesSummaryResponse GetSalesSummary(SalesSummaryFilter request);

        SalesDetailResponse GetSalesDetails(SalesDetailFilter request);

        ConsignmentSummaryResponse GetConsignmentSummary(ConsignmentSummaryFilter request);

        ConsignmentReturnDetailResponse GetConsignmentReturnDetail(ConsignmentReturnDetailFilter request);
    }
}