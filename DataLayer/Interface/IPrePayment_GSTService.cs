using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPrePayment_GSTService
    {
        PrePayment_GSTResponse Save(PrePayment_GST model);
        PrePayment_GSTResponse Update(PrePayment_GSTUpdate model);
        PrePayment_GSTListResponse GetPrePaymentList();
        PrePayment_GSTListHeaderResponse GetPrePaymentById(int id);
        PrePayment_GSTResponse commit(PrePayment_GSTUpdate model);
        PrePayment_GSTResponse Delete(int id);
        PrePaymentGSTLastDocno GetLastDocNo();
    }
}
