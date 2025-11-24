using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscPayment_GSTService
    {
        MiscpaymentGSTResponse Save(MiscPayment_GST model);
        MiscpaymentGSTResponse Edit(MiscPayment_GSTUpdate model);
        MiscPaymentGSTSelectedView GetMiscPaymentById(int id);
        MiscPaymentListGSTResponse GetMiscPaymentList();
        MiscpaymentGSTResponse commit(MiscPayment_GSTUpdate model);
        MiscGSTLastDocno GetLastDocNo();
        MiscpaymentGSTResponse Delete(int id);
    }
}
