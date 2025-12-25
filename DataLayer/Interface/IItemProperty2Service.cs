using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemProperty2Service
    {
        public List<ItemProperty2> GetAllItemProperty2(ItemPropertyList request);
        public Int32 SaveData(ItemProperty2 company);
        public ItemProperty2 GetItems(int id);
        public bool DeleteItemProperty2(int id);
    }
}
