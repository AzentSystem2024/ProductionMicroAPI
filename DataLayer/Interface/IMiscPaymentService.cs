using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscPaymentService
    {
         MiscpaymentResponse Save(MiscPayment model);
        MiscpaymentResponse Edit(MiscPaymentUpdate model);
        MiscPaymentSelectedView GetMiscPaymentById(int id);

        MiscPaymentListResponse GetMiscPaymentList();

    }
}
