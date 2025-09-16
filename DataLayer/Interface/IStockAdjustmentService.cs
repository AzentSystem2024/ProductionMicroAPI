using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IStockAdjustmentService
    {
        
       // StockAdjustmentListResponse GetAllStockAdjustments(int storeId, int companyId);
        int SaveData(StockAdjustment stockAdjustment);
         int EditData(StockAdjustment stockAdjustment);
        //StockAdjustmentListResponse GetStockAdjustment(int adjId);
        // bool DeleteStockAdjustment(int adjId);
        StockItemListResponse GetStockAdjustmentItems(int storeId);
    }
}

