using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IInvoiceService
    {
        InvoiceResponse insert(Invoice model);
        TransferGridResponse GetTransferData();
        InvoiceHeaderResponse GetSaleInvoiceHeaderData();
    }
}
