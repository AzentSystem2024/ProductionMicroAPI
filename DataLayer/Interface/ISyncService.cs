using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISyncService
    {
        SyncResponse Insert(Sync model);
        SyncResponse UploadPackProduction(PackProductionSync model);
        SyncResponse UploadProductionDN(ProductionDN model);
        //SyncResponse UploadProductionTransferIn(ProductionTransferIn model);
       // SyncResponse UploadProductionDN(ProductionDN model);
        SyncResponse UploadProductionDR(ProductionDR model);
        ProductionViewResponse GetProductionById(int id);
        ProductionListResponse GetProductionList(ProductionListRequest model);
        ProductionViewResponse GetBoxProductionById(int id);
        ProductionListResponse GetBoxProductionList(ProductionListRequest model);
        DNListResponse GetDNList(ProductionListRequest model);
        DNViewResponse GetProductionDNById(int id);
        GRNUploadResponse SaveProductionTransferInGRN(ProductionTransferInGRN model);
        SyncResponse SaveDispatch(DispatchSave model);
    }
}
