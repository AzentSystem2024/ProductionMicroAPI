using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepreciationService
    {
        DepreciationResponse GetFixedAssetsList();
    }
}
