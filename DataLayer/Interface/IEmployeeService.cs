using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployees(int companyId);
        public Int32 SaveData(Employee company);
        public Employee GetItems(int id);
        public bool DeleteEmployees(int id);
    }
}
