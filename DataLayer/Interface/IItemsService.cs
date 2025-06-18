using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IItemsService
    {
        ItemResponse Insert(Item item);
        ItemResponse Update(ItemUpdate item);
        ItemResponse GetItemById(int id);
        ItemListResponse GetLogList(int? id = null);
        ItemResponse DeleteItemData(int id);
    //    List<Item> GetItem(ItemInput itemInput);
    //    ItemDownloadOutput DownloadItem(ItemDownloadInput vInput);
    //    PendingItemResponse GetPendingItem();
    }
}
