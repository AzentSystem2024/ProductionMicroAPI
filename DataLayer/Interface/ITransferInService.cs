using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITransferInService
    {
        public List<TransferInItemList> GetTransferInItems(TransferInInput input);
        public Int32 Insert(TransferIn transferIn);
        public bool Delete(int id);
        TransferInListsResponse List();

    }
}
