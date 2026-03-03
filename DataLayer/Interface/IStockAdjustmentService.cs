using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IStockAdjustmentService
    {
        
        StockAdjustmentListResponse GetAllStockAdjustments(StockAdjListRequest request);
        int SaveData(StockAdjustment stockAdjustment);
        StockAdjustmentResponse EditData(StockAdjustmentUpdate stockAdjustment);
        StockAdjustmentDetailResponse GetStockAdjustment(int adjId);
        bool DeleteStockAdjustment(int adjId);
        StockItemListResponse GetStockAdjustmentItems(StockAdjustmentRequest request);
        StockAdjustmentResponse ApproveStockAdjustment(StockAdjustmentApproval stockAdjustment);
    }
}

