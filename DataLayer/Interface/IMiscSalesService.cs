using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IMiscSalesService
    {
        MiscSalesResponse Insert(MiscSales input);
        MiscSalesResponse GetMiscSalesList(MiscSalesListRequest input);
        MiscSalesResponse Update(MiscSales input);
        MiscSalesResponse Commit(MiscSales input);
        MiscSalesViewResponse GetMiscSalesById(int Id);
        MiscSalesResponse Delete(int id);
    }
}
