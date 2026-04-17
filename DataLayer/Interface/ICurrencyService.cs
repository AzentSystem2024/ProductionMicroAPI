using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ICurrencyService
    {
        public List<Currency> GetAllCurrency();
        public Int32 SaveData(Currency company);
        public Currency GetItems(int id);
        public bool DeleteCurrency(int id);
    }
}
