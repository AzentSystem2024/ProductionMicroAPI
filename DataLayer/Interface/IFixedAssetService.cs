using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IFixedAssetService
    {
       public FixedAssetListResponse GetFixedAssetList();
        FixedAssetSaveResponse SaveData(FixedAsset fixedAsset);
        FixedAssetSaveResponse UpdateData(FixedAsset fixedAsset);
        FixedAssetSelectResponse GetFixedAssetbyId(int? id = null);
        FixedAssetSaveResponse Delete(int id);
        FixedResponse Save(ASSET asset);

    }
}
