using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IARManualMatchingService
    {
        public ARReceiptResponse receiptList(ARReceiptInput vinput);
        public ARInvoiceResponse invoiceList(ARInvoiceInput vinput);
    }
}
