using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDenialMasterService
    {
        IEnumerable<DenialMaster> GetAllDenial(int intUserID);
        public Int32 Insert(DenialMaster denialMaster, Int32 userID);
        public List<DenialMaster> GetItems(int id);
        public Int32 Update(DenialMaster denialMaster, Int32 userID);
        public bool DeleteDenialMaster(int Id, int userID);
    }
}
