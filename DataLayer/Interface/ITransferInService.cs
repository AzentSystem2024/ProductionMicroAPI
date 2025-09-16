using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITransferInService
    {
        public List<TransferInItemList> GetTransferInItems(TransferInInput input);
        public Int32 Insert(TransferIn transfeIn);
        public Int32 Update(TransferIn transfeIn);
        public bool Delete(int id);
    }
}
