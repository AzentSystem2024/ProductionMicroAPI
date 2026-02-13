using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IStockMovementRptService
    {
        StockMovementRptResponse GetStockMovementReport(StockMovementRequest request);
        StockMovementDrilldownResponse GetStockMovementDrilldown(StockMovementDrillDownRequest request);
    }
}
