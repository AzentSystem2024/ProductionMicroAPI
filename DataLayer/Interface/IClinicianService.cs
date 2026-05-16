using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IClinicianService
    {
        public List<Clinician> GetAllClinicians(int intUserID);
        public Int32 Insert(Clinician clinician, Int32 userID);
        public Int32 Update(Clinician clinician, Int32 userID);
        public List<Clinician> GetItems(int id);
        public bool DeleteClinicians(int Id, int userID);
    }
}
