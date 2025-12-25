using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IACDefaultsService
    {
        AcDefaultsListResponse GetACDefaultsList(AcDefaultsListReq request);
        AcDefaultsListResponse Save(ACDefaults request);
    }
}
