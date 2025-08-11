using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepreciationService
    {
        DepreciationResponse GetFixedAssetsList();
        DepreciationListResponse GetList();
        //DepreciationResponse InsertDepreciation();
        public int InsertDepreciation(DepreciationInsertRequest request);
       public int UpdateDepreciation(DepreciationUpdateRequest request);

    }
}
