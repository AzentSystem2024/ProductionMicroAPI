using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface ISubDepartmentService
    {
        public List<SubDepartment> GetAllDepartments();
        public int SaveDepartment(SubDepartment subdepartment);
        public SubDepartment GetDepartmentById(int id);
        public bool DeleteDepartment(int id);
    }
}
