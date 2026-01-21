using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemStockRptService
    {
        ItemStockViewResponse GetItemStockView(ItemStockRptRequest request);
    }
}
