using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITendersService
    {
        public List<Tenders> GetAllTender();
        public Int32 SaveData(Tenders company);
        public Tenders GetItems(int id);
        public bool DeleteTenders(int id);
    }
}
