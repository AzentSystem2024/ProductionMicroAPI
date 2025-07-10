using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IInvoiceService
    {
        InvoiceResponse insert(Invoice model);
        InvoiceResponse Update(InvoiceUpdate model);
        TransferGridResponse GetTransferData();
        InvoiceHeaderResponse GetSaleInvoiceHeaderData();
        InvoiceHeaderSelectResponse GetSaleInvoiceById(int id);
        InvoiceResponse CommitInvoice(CommitInvoiceRequest request);
        InvResponse GetInvoiceNo();
        InvoiceResponse Delete(int id);
    }
}
