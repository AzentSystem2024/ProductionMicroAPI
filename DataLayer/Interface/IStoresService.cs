using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IStoresService
    {
        public List<Stores> GetAllStores();
        public Int32 SaveData(Stores company);
        public Stores GetItems(int id);
        public bool DeleteStores(int id);
    }
}
