using MicroApi.Models;
using System.Data;

namespace MicroApi.DataLayer.Interface
{
    public interface IPurchaseReportService
    {
        PurchaseSummaryResponse GetPurchaseSummary(PurchaseReport filter);
        ItemWisePurchaseResponse GetItemWisePurchaseReport(ItemWisePurchaseReportRequest filter);
    }
}
