using MicroApi.Models;
using System.Drawing;

namespace MicroApi.DataLayer.Interface
{
    public interface IPackingService
    {
        List<ProductionUnit> GetProductionUnits();
       // string GenerateCombinationString(string artNo, string color, int categoryID, int unitID, int pairQty);
        List<Supplier> GetSuppliers();
        List<ArticleSize> GetArticleSizesForCombination(ArticleSizeCombinationRequest request);
        PackingResponse Insert(PackingMasters packing);
        PackingResponse Update(PackingUpdate packing);
        PackingSelectResponse GetPackingById(int id);
        PackingListResponses GetPackingList(PackingListReq request);
        PackingResponse DeletePackingData(int id);
        string GetAliasNo();
        PackingResponse ChangeStandardPrice(ChangeStandardPriceModel model);
        List<PackingPriceLog> GetPackingPriceLog(PackingPriceLogrequest request);
    }
}
