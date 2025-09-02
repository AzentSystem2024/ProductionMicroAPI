using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemProperty3Service
    {
        public List<ItemProperty3> GetAllItemProperty3();
        public Int32 SaveData(ItemProperty3 company);
        public ItemProperty3 GetItems(int id);
        public bool DeleteItemProperty3(int id);
    }
}
