using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepartmentService
    {
        public List<Department> GetAllDepartments(Departmentlist request);
        public int SaveDepartment(Department department);
        public Department GetDepartmentById(int id);
        public bool DeleteDepartment(int id);
    }
}
