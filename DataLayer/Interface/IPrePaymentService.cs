using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPrePaymentService
    {
        PrePaymentResponse Save(PrePayment model);
        PrePaymentResponse Update(PrePaymentUpdate model);
        PrePaymentListResponse GetPrePaymentList();
        PrePaymentListHeaderResponse GetPrePaymentById(int id);

    }
}
