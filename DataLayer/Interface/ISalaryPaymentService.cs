using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryPaymentService
    {
        SalaryPaymentResponse Insert(SalaryPayment model);
        SalaryPendingResponse GetPendingSalaryList(SalaryPendingRequest request);
    }
}
