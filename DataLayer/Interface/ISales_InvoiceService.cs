using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISales_InvoiceService
    {
        SalesInvoiceResponse insert(Sales_Invoice model);
        SalesInvoiceResponse Update(Sale_InvoiceUpdate model);
        DeliveryGridResponse GetTransferData(DeliveryInvoiceRequest request);
        SalesInvoiceHeaderResponse GetSaleInvoiceHeaderData();
        SalesInvselectResponse GetSaleInvoiceById(int id);
        public int Delete(int id);
        SalesInvoiceResponse Approve(Sale_InvoiceUpdate model);

    }
}
