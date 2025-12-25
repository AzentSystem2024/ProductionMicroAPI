using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IGetSystemDataService
    {
        GetSystemDataResponse GetSystemInfo();
    }
}
