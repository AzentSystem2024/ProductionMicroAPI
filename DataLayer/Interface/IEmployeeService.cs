using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployees(int? companyId);
        Int32 SaveData(Employee employee);
        bool UpdateEmployee(Employee employee);
        public Employee GetItems(int id);
        public bool DeleteEmployees(int id);
    }
}
