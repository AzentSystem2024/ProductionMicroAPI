using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeSalaryService
    {
        public EmployeeListResponse GetAllEmployeeSalaries(int EMPID, int COMPANYID);
        public Int32 SaveData(EmployeeSalarySave salary);
        public Int32 EditData(EmployeeSalarySave salary);
        public EmployeeListResponse GetItem(int id);
        public bool DeleteEmployeeSalary(int id);
        public EmployeeSalarySettingsListResponse GetEmployeeSalarySettings(int filterAction);
    }
}
