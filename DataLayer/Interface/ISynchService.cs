using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISynchService
    {
        SynchResponse UploadData(Synch model);
        SynchDownloadResponse DownloadData(SynchDownload model);

    }
}
