using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryPaymentService
    {
        SalaryPaymentResponse insert(SalaryPayment model);

    }
}
