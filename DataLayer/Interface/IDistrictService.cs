using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDistrictService
    {
        DistrictResponse Insert(District district);
        DistrictResponse Update(District district);
        DistrictListResponse GetDistrictList();
        DistrictResponse GetDistrictById(int id);
        DistrictResponse Delete(int id);
    }
}
