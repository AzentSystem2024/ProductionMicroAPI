using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeEOSService
    {
        public EmployeeEOSLogListResponseData GetAllEmployeeEOS();
        public saveEmployeeEOSResponseData SaveData(saveEmployeeEOSData eosData);
        public saveEmployeeEOSData selectEmployeeEOSData(int id);
        public saveEmployeeEOSResponseData UpdateData(saveEmployeeEOSData advData);
        public saveEmployeeEOSResponseData DeleteEmployeeEOS(int id);
        public saveEmployeeEOSResponseData VerifyData(saveEmployeeEOSData advData);
        public saveEmployeeEOSResponseData ApproveData(saveEmployeeEOSData advData);
        public EOSEmployeeData getEOSEmployeeData(EOSEmployeeInput inp);

    }
}
