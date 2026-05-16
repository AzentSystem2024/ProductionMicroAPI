using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ICptMasterService
    {
        public List<CptMaster> GetAllCptMasters(int intUserID);
        public Int32 Insert(CptMaster cptmaster, Int32 userID);
        public Int32 Update(CptMaster cptmaster, Int32 userID);
        public List<CptMaster> GetItems(int id);
        public bool DeleteCptMaster(int Id, int userID);
        public subDepartmentResponse getSubDepartment(CptMaster vInput);
    }
}
