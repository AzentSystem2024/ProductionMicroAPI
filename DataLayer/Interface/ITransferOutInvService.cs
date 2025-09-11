using MicroApi.Models;
using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITransferOutInvService
    {
        public Int32 Insert(TransferOutInv transferOut);
        public List<ItemInfo> GetItemInfo(ItemRequest request);

    }
}
