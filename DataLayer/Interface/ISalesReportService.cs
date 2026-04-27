using MicroApi.Models;
using System.Data;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalesReportService
    {
        // SalesSummaryResponse GetSalesSummary(SalesSummaryFilter request);
        DataTable GetSalesSummary(SalesSummaryFilter filter);
        //SalesDetailResponse GetSalesDetails(SalesDetailFilter request);
        DataTable GetSalesDetails(SalesDetailFilter request);

        ConsignmentSummaryResponse GetConsignmentSummary(ConsignmentSummaryFilter request);

        ConsignmentReturnDetailResponse GetConsignmentReturnDetail(ConsignmentReturnDetailFilter request);
        ItemWiseSalesResponse GetItemWiseSales(ItemWiseSalesFilter request);

        //ItemWiseSalesSummaryResponse GetItemWiseSalesSummary(ItemWiseSalesSummaryFilter request);
        DataTable GetItemWiseSalesSummary(ItemWiseSalesSummaryFilter request);
        DataTable GetDiscountWiseSales(DiscountWiseSalesFilter request);
        //DiscountWiseSalesResponse
        DataTable GetTenderReport(TenderReportFilter request);

        DataTable GetTenderSummary(TenderSummaryFilter request);
    }
}