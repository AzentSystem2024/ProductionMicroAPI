using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemsService
    {
        public List<Items> GetAllItems(ItemListRequest request);
        public (int flag, string message) Insert(Items company);
        public bool Update(Items company);
        public Items GetItems(int id);
        public bool DeleteItem(int id);
        public ItemsResponse Alias(ALIAS_DUPLICATE vInput);
    }
}
