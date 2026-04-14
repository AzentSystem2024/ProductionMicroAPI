using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalesInvoiceService
    {
        SalesResponse GetSalesInvoiceItem(SalesInvoiceRequest request);
        SalesInvoiceInsertResponse Insert(SalesInvoiceInsertRequest input);
        SalesInvoiceInsertResponse Update(SalesInvoiceInsertRequest input);
        SalesInvoiceListResponse GetSalesInvoiceHeaderList(InvoiceListRequest request);
        SalesInvoiceViewResponse GetSalesInvoiceById(int id);
        SalesInvoiceInsertResponse Commit(SalesInvoiceInsertRequest input);
        SalesInvoiceInsertResponse Delete(int id);
    }
}
