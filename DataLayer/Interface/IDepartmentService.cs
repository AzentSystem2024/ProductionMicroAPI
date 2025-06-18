using MicroApi.Models;

namespace MicroApi.DataLayer.Interface
{
    public interface IDepartmentService
    {
        DepartmentResponse Insert(Department department);
        DepartmentResponse Update(DepartmentUpdate department);
        DepartmentResponse GetDepartmentById(int id);
        DepartmentListResponse GetLogList(int? id = null);
        DepartmentResponse DeleteDepartmentData(int id);
    }
}
