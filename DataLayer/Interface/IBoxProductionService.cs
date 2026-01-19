using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IBoxProductionService
    {
        BoxProdResponse Insert(BoxProduction model);
        PackingBOMResponse GetPackingBomList(PackingBOMRequest model);
        BoxProductionSelectResponse GetProductionById(int id);
        BoxProductionListResponse packingprodlist(BoxProductionListRequest model);
    }
}
