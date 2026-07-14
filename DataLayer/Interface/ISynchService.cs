using MicroApi.Models;
using System.Data;

namespace MicroApi.DataLayer.Interface
{
    public interface ISynchService
    {
        SynchResponse UploadData(Synch model);
        SynchDownloadResponse DownloadData(SynchDownload model);
        List<PendingStoreResponse> GetSynchPendingStores();
        SynchResponse UpdateLastSynchTime(UpdateLastSynchTimeRequest request);
    }
}
