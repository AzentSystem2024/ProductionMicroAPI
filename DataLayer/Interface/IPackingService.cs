using MicroApi.Models;
using System.Drawing;

namespace MicroApi.DataLayer.Interface
{
    public interface IPackingService
    {
        List<ProductionUnit> GetProductionUnits();
       // string GenerateCombinationString(string artNo, string color, int categoryID, int unitID, int pairQty);
        List<Supplier> GetSuppliers();
        List<ArticleSize> GetArticleSizesForCombination(string artNo, string color, int categoryID, int unitID);
        PackingResponse Insert(PackingMasters packing);
        PackingResponse Update(PackingUpdate packing);
        PackingResponse GetPackingById(int id);
        PackingListResponses GetPackingList();
        PackingResponse DeletePackingData(int id);
        
       
    }
}
