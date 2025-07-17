using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployees(int? companyId);
        public int SaveEmployee(Employee employee);
        bool UpdateEmployee(EmployeeUpdate employee);
        public EmployeeUpdate GetItems(int id);
        public bool DeleteEmployees(int id);
    }
}
