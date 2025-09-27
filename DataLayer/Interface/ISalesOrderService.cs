using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalesOrderService
    {
        SalesOrderListResponse GetAllSalesOrders();
        SalesOrderDetailSelectResponse GetSalesOrder(int soId);
        int SaveData(SalesOrder salesOrder);
        SalesOrderResponse EditData(SalesOrderUpdate salesOrder);
        bool DeleteSalesOrder(int soId);
        ItemListResponse GetSalesOrderItems(SalesOrderRequest request);
        SalesOrderResponse ApproveSalesOrder(SalesOrderUpdate request);
        SOQUOTATIONLISTResponse GetSOQUOTATIONLIST();
        LatestVocherNOResponse GetLatestVoucherNumber();
    }
}
