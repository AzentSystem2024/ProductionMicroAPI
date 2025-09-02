using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemsService
    {
        public List<Items> GetAllItems(DateRequest request);
        public bool Insert(Items company);
        public bool Update(Items company);
        public Items GetItems(int id);
        public bool DeleteItem(int id);
        public ItemsResponse Alias(ALIAS_DUPLICATE vInput);
    }
}
