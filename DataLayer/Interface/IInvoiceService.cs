using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IInvoiceService
    {
        InvoiceResponse insert(Invoice model);
        InvoiceResponse Update(InvoiceUpdate model);
        TransferGridResponse GetTransferData(TransferInvoiceRequest request);
        InvoiceHeaderResponse GetSaleInvoiceHeaderData(InvoiceListRequest request);
        InvoiceHeaderSelectResponse GetSaleInvoiceById(int id);
        InvoiceResponse commit(InvoiceUpdate model);
        InvResponse GetInvoiceNo();
        InvoiceResponse Delete(int id);
        List<InvoiceCust_stateName> Getcustlist(InvoiceListRequest request);
    }
}
