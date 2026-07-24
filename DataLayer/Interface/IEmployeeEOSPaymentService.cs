using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeEOSPaymentService
    {
        // List all EOS Payments
        EmployeeEOSPaymentListResponseData GetAllEmployeeEOSPayments();
        // EOSPaymentResponse GetAllEmployeeEOSPayments(EOSPaymentRequest request);
        //GetAllEmployeeEOSPayments
        // Save a new EOS Payment
        SaveEmployeeEOSPaymentResponseData SaveData(SaveEmployeeEOSPaymentData paymentData);

        // Select a payment by Id
        SaveEmployeeEOSPaymentData SelectEmployeeEOSPaymentData(int id);

        // Update an existing payment
        SaveEmployeeEOSPaymentResponseData UpdateData(SaveEmployeeEOSPaymentData paymentData);

        // Delete a payment
        SaveEmployeeEOSPaymentResponseData DeleteEmployeeEOSPayment(int id);

        // Verify a payment
        SaveEmployeeEOSPaymentResponseData VerifyData(SaveEmployeeEOSPaymentData paymentData);

        // Approve a payment
        SaveEmployeeEOSPaymentResponseData ApproveData(SaveEmployeeEOSPaymentData paymentData);
      //  EOSPaymentResponse GetEmployeeEOSPayment(EOSPaymentRequest request);
    }
}
