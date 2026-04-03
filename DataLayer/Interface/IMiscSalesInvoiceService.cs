using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscSalesInvoiceService
    {
        MiscSalesInvoiceResponse insert(MiscSalesInvoiceSave input);
        InvoiceResponse Update(InvoiceUpdate model);
        TransferGridResponse GetTransferData(TransferInvoiceRequest request);
        MiscSalesInvoiceResponse getMisSalesInvoiceData(InvoiceListRequest request);
        MiscSalesInvoiceSave GetMiscSaleInvoiceById(int id);
        InvoiceResponse commit(InvoiceUpdate model);
        InvResponse GetInvoiceNo();
        InvoiceResponse Delete(int id);
        List<InvoiceCust_stateName> Getcustlist(InvoiceListRequest request);
        public getItemsResponse getItems(getItemsInput request);
    }
}
