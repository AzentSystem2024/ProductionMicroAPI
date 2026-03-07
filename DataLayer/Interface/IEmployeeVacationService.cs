using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeVacationService
    {
        public EmployeeVacationLogListResponseData GetAllEmployeeVacation();
        public saveEmployeeVacationResponseData SaveData(saveEmployeeVacationData vacationData);
        public saveEmployeeVacationData selectEmployeeVacationData(int id);
        public saveEmployeeVacationResponseData UpdateData(saveEmployeeVacationData vacationData);
        public saveEmployeeVacationData DeleteEmployeeVacation(int id);
        public saveEmployeeVacationResponseData VerifyData(saveEmployeeVacationData vacationData);
        public saveEmployeeVacationResponseData ApproveData(saveEmployeeVacationData vacationData);
        public EmployeeLeaveCreditResponse GetEmployeeLeaveCredit(EmployeeLeaveCreditRequest request);
    }
}
