using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDeliveryTermsService
    {
        public List<DeliveryTerms> GetAllDeliveryTerms();
        public Int32 SaveData(DeliveryTerms company);
        public DeliveryTerms GetItems(int id);
        public bool DeleteDeliveryTerms(int id);
    }
}
