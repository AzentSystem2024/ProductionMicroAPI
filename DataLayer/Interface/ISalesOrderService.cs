using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalesOrderService
    {
        SalesOrderListResponse GetAllSalesOrders(SOListRequest request);
        SalesOrderDetailSelectResponse GetSalesOrder(int id);
        int SaveData(SalesOrder salesOrder);
        SalesOrderResponse EditData(SalesOrderUpdate salesOrder);
        bool DeleteSalesOrder(int soId);
        ItemListsResponse GetSalesOrderItems();
        ItemListsResponse GetarticleType();
        ItemListsResponse Getcategory(SalesOrderRequest request);
        ItemListsResponse GetArtNo(SalesOrderRequest request);
        ItemListsResponse GetColor(SalesOrderRequest request);
        ItemListsResponse GetPacking(SalesOrderRequest request);
        PairResponse GetPairQtyByPackingId(PackingPairRequest request);
        WarehouseResponse GetWarehouseByCustId(SOQUOTATIONRequest request);
        SalesOrderResponse ApproveSalesOrder(SalesOrderUpdate request);
        SOQUOTATIONLISTResponse GetSOQUOTATIONLIST(SOQUOTATIONRequest request);
        LatestVocherNOResponse GetLatestVoucherNumber();
        public List<Subdealers> GetSubdealer(SubdealerRequest id);

    }
}
