using MicroApi.Models;
using MicroApi.DataLayer.Interface;


namespace MicroApi.DataLayer.Interface
{
    public interface IReceiptService
    {
        ReceiptResponse insert(Receipt model);
        PendingInvoiceListResponse GetPendingInvoiceList();
        ReceiptListResponse GetReceiptList();
        ReceiptResponse Update(ReceiptUpdate model);
        ReceiptSelectResponse GetReceiptById(int id);
        ReceiptResponse CommitReceipt(CommitReceiptRequest request);
        RecResponse GetReceiptNo();
        ReceiptResponse Delete(int id);
        ReceiptLedgerListResponse GetLedgerList();


    }
}
