using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISupplierPaymentService
    {
        SupplierPaymentResponse insert(SupplierPayment model);
        SupplierPaymentResponse Update(SupplierPaymentUpdate model);
        SupplierPaymentListResponse GetPaymentList();
        SupplierSelectResponse GetSupplierById(int id);
        PendingInvoiceResponse GetPendingInvoiceList();
        SupplierPaymentResponse Commit(CommitRequest request);
        SupplierVoucherResponse GetSupplierNo();


    }
}
