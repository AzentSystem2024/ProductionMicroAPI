using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDocSettingsService
    {
        DocSettingsResponse Insert(DocSettings model);
        DocSettingsListResponse List(DocSettingsListRequest request);
    }
}
