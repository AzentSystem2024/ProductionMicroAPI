using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IBoxProductionService
    {
        BoxProdResponse Insert(BoxProduction model);
        BoxProdResponse Update(BoxProductionUpdate model);
        BoxProdResponse commit(BoxProductionUpdate model);
        BoxProdResponse Delete(int id);
        PackingBOMResponse GetPackingBomList(PackingBOMRequest model);
        BoxProductionSelectResponse GetProductionById(int id);
        BoxProductionListResponse packingprodlist(BoxProductionListRequest model);
    }
}
