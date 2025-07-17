using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeSalaryService
    {
        public EmployeeListResponse GetAllEmployeeSalaries();
        public Int32 SaveData(EmployeeSalarySave salary);
        public Int32 EditData(EmployeeSalaryUpdate salary);
        public EmployeeSalaryUpdate GetItem(int id);
        public bool DeleteEmployeeSalary(int id);
        public EmployeeSalarySettingsListResponse GetEmployeeSalarySettings(int filterAction);
    }
}
