using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ILandedCostService
    {
        public List<LandedCost> GetAllLandedCost();
        public Int32 SaveData(LandedCost company);
        public LandedCost GetItems(int id);
        public bool DeleteLandedCost(int id);
    }
}
