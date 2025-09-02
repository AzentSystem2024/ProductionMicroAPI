using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemProperty5Service
    {
        public List<ItemProperty5> GetAllItemProperty5();
        public Int32 SaveData(ItemProperty5 company);
        public ItemProperty5 GetItems(int id);
        public bool DeleteItemProperty5(int id);
    }
}
