using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IInsuranceService
    {
        InsuranceResponse Insert(Insurance insurance);
        InsuranceResponse Update(InsuranceUpdate insurance);
        InsuranceResponse GetInsuranceById(int id);
        InsuranceListResponse GetLogList(int? id = null);
        InsuranceResponse DeleteInsuranceData(int id);
    }
}
