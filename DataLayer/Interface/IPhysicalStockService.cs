using MicroApi.Models;
using System.Data;

namespace MicroApi.DataLayer.Interface
{
    public interface IPhysicalStockService
    {
        PhysicalStockListResponse GetAllPhysicalStocks();
        int SaveData(PhysicalStock physicalStock);
        PhysicalStockResponse EditData(PhysicalStockUpdate physicalStock);
        PhysicalStockDetailResponse GetPhysicalStock(int physicalId);
        bool DeletePhysicalStock(int physicalId);
        StockItemListResponse GetPhysicalStockItems(PhysicalStockRequest request);
        PhysicalStockResponse ApprovePhysicalStock(PhysicalStockApproval physicalStock);
        List<StockItems> GetFilteredItems(FilteredItemsRequest request);
    }
}
