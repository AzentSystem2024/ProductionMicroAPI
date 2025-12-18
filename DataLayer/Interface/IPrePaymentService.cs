using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPrePaymentService
    {
        PrePaymentResponse Save(PrePayment model);
        PrePaymentResponse Update(PrePaymentUpdate model);
        PrePaymentListResponse GetPrePaymentList(PrePaymentListRequest request);
        PrePaymentListHeaderResponse GetPrePaymentById(int id);
        PrePaymentResponse commit(PrePaymentUpdate model);
        PrePaymentResponse Delete(int id);
        PrePaymentLastDocno GetLastDocNo();

    }
}
