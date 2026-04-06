using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IPaymentTermService
    {
        public List<PaymentTerms> GetAllPaymentTerms();
        public Int32 SaveData(PaymentTerms company);
        public PaymentTerms GetItems(int id);
        public bool DeletePaymentTerms(int id);
    }
}
