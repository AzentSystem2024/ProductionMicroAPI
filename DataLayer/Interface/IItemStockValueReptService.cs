using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemStockValueReptService
    {
        ItemStockValueReportResponse GetItemStockValueReport(ItemStockValueReportRequest request);
    }
}
