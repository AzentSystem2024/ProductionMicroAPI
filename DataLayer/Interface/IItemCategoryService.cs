using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemCategoryService
    {
        public List<ItemCategory> GetAllItemCategory();
        public Int32 SaveData(ItemCategory company);
        public ItemCategory GetItems(int id);
        public bool DeleteItemCategory(int id);
    }
}
