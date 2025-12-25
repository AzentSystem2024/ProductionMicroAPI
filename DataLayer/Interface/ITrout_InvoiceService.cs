using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITrout_InvoiceService
    {
        Trout_InvoiceResponse insert(Trout_Invoice model);
        Trout_InvoiceResponse update(Trout_InvoiceUpdate model);
        PendingDeliverydataResponse GetTransferData(PendingDeliverydataRequest request);
        Trout_InvoiceListResponse GetSaleInvoiceHeaderData(Trout_InvoiceListRequest request);
        Trout_InvoiceSelectResponse GetSaleInvoiceById(int id);
        Trout_InvoiceResponse commit(Trout_InvoiceUpdate model);
        TroutInvResponse GetInvoiceNo();
        Trout_InvoiceResponse Delete(int id);
        public List<TroutCust_stateName> Getcustlist(Trout_InvoiceListRequest request);
    }
}
