using MicroApi.Models;
using System.Data;

namespace MicroApi.DataLayer.Interface
{
    public interface IPhysicalStockService
    {
        PhysicalStockListResponse GetAllPhysicalStocks(PhysicalStockListRequest request);
        int SaveData(PhysicalStock physicalStock);
        PhysicalStockResponse EditData(PhysicalStockUpdate physicalStock);
        PhysicalStockSelectDetailResponse GetPhysicalStock(int physicalId);
        bool DeletePhysicalStock(int physicalId);
        StockItemListResponse GetPhysicalStockItems(PhysicalStockRequest request);
        PhysicalStockResponse ApprovePhysicalStock(PhysicalStockApproval physicalStock);
        List<StockItems> GetFilteredItems(FilteredItemsRequest request);
        ItemCodeListResponse GetPhysicalStockItemsByItemCodes(ItemCodeRequest request);
        List<HistoryModel> GetPhysicalStockHistory();
        PhysicalStockLatestVocherNOResponse GetLatestVoucherNumber();
    }
}
