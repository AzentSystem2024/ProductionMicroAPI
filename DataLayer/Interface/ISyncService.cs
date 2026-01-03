using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISyncService
    {
        SyncResponse Insert(Sync model);
        SyncResponse UploadPackProduction(PackProductionSync model);
        SyncResponse UploadProductionTransferOut(ProductionTransferOut model);
        SyncResponse UploadProductionTransferIn(ProductionTransferIn model);

    }
}
