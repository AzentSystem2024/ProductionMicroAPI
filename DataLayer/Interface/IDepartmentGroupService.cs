using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepartmentGroupService
    {
        public List<DepartmentGroup> GetAllDepartmentGroups(DepartmentGroupList request);
        public int SaveDepartmentGroup(DepartmentGroup department);
        public DepartmentGroup GetDepartmentGroupById(int id);
        public bool DeleteDepartmentGroup(int id);
    }
}
