using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemProperty1Service
    {
        public List<ItemProperty1> GetAllItemProperty1(ItemPropertyList request);
        public Int32 SaveData(ItemProperty1 company);
        public ItemProperty1 GetItems(int id);
        public bool DeleteItemProperty1(int id);
    }
}
