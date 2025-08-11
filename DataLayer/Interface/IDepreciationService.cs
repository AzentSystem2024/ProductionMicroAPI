using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepreciationService
    {
        DepreciationResponse GetFixedAssetsList();
        DepreciationListResponse GetList();
        //DepreciationResponse InsertDepreciation();
        DepreciationResponse InsertDepreciation(DepreciationInsertRequest request);

    }
}
