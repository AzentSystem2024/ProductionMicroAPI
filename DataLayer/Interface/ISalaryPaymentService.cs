using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISalaryPaymentService
    {
        SalaryPaymentResponse Insert(SalaryPayment model);
        SalaryPendingResponse GetPendingSalaryList(SalaryPendingRequest request);
        SalaryPaymentListResponse GetsalaryPaymentList();
        SalPayLastDocno GetLastDocNo();
        SalaryPaymentDetailResponse GetSalaryPaymentById(int id);
        SalaryPaymentResponse update(SalaryPaymentUpdate model);
        SalaryPaymentResponse commit(SalaryPaymentUpdate model);


    }
}
