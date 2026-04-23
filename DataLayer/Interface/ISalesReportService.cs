using MicroApi.Models;
using System.Data;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalesReportService
    {
        // SalesSummaryResponse GetSalesSummary(SalesSummaryFilter request);
        DataTable GetSalesSummary(SalesSummaryFilter filter);
        SalesDetailResponse GetSalesDetails(SalesDetailFilter request);

        ConsignmentSummaryResponse GetConsignmentSummary(ConsignmentSummaryFilter request);

        ConsignmentReturnDetailResponse GetConsignmentReturnDetail(ConsignmentReturnDetailFilter request);
        ItemWiseSalesResponse GetItemWiseSales(ItemWiseSalesFilter request);

        ItemWiseSalesSummaryResponse GetItemWiseSalesSummary(ItemWiseSalesSummaryFilter request);

        DiscountWiseSalesResponse GetDiscountWiseSales(DiscountWiseSalesFilter request);

        TenderReportResponse GetTenderReport(TenderReportFilter request);

        TenderSummaryResponse GetTenderSummary(TenderSummaryFilter request);
    }
}