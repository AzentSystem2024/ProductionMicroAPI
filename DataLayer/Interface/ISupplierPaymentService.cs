using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISupplierPaymentService
    {
        SupplierPaymentResponse insert(SupplierPayment model);
        SupplierPaymentResponse Update(SupplierPaymentUpdate model);
        SupplierPaymentListResponse GetPaymentList(SuppListRequest request);
        SupplierSelectResponse GetSupplierById(int id);
        PendingInvoiceResponse GetPendingInvoiceList(PendingInvoiceRequest request);
        SupplierPaymentResponse commit(SupplierPaymentUpdate model);
        SupplierVoucherResponse GetSupplierNo();
        SupplierPaymentResponse Delete(int id);
        PDCListResponse GetPDCListBySupplierId(SupplierIdRequest request);


    }
}
