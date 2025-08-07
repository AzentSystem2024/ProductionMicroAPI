using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscPaymentService
    {
         MiscpaymentResponse Save(MiscPayment model);
        MiscPaymentListResponse GetMiscPaymentList();

    }
}
