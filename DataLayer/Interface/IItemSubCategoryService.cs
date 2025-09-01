using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemSubCategoryService
    {
        public List<ItemSubCategory> GetAllItemSubCategory();
        public Int32 SaveData(ItemSubCategory company);
        public ItemSubCategory GetItems(int id);
        public bool DeleteItemSubCategory(int id);
    }
}
