using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ITransferService
    {
        TransferListResponse GetTransferList();

    }
}
