using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeSalaryService
    {
        public EmployeeListResponse GetAllEmployeeSalaries(int empId, int companyId);
        public Int32 SaveData(EmployeeSalarySave salary);
        public Int32 EditData(EmployeeSalarySave salary);
        public EmployeeListResponse GetItem(int empId, string effectfrom);
        public bool DeleteEmployeeSalary(int batchId);
        public EmployeeSalarySettingsListResponse GetEmployeeSalarySettings(int filterAction, int companyId);
    }
}
