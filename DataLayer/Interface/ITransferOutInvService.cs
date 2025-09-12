using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITransferOutInvService
    {
        public Int32 Insert(TransferOutInv transferOut);
        public List<ItemInfo> GetItemInfo(ItemRequest request);
        public Int32 Update(TransferOutInvUpdate transferOut);
        public List<TransferOutDetailList> GetTransferOutList();
        TransferOutInvUpdate GetTransferOut(int id);

    }
}
